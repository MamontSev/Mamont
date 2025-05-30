using System;

using Mamont.Gameplay.UI.Health;
using Mamont.Log;

using Manmont.Tools;


using UnityEngine;

namespace Mamont.Gameplay.Control.Damage
{
	public interface IDamagableObjHealthControl
	{
		void DamageMe( float damage , DamageType damageType );
		void HealMe( float healCount );
		void HealMePercent( float perc );
		bool MayDamageMe();
	}
	public class DamagableObjHealthControl:IDamagableObjHealthControl
	{
		private readonly IHealthSliderFactory _healthSliderFactory;
		private readonly ILogService _logService;
		public DamagableObjHealthControl
		(
			IHealthSliderFactory _healthSliderFactory ,
			ILogService _logService
		)
		{
			this._healthSliderFactory = _healthSliderFactory;
			this._logService = _logService;
		}

		private Func<bool> IsEvasion;
		private Func<bool> MayChangeHealth;
		private float _startHealth;
		private Transform _sliderTransform;
		public void Init
		(
			Func<bool> IsEvasion ,
			Func<bool> MayChangeHealth ,
			float _startHealth ,
			Transform _sliderTransform 
		)
		{
			this.IsEvasion = IsEvasion;
			this.MayChangeHealth = MayChangeHealth;
			this._sliderTransform = _sliderTransform;

			if( _startHealth <= 0.0f )
			{
				_logService.LogError($"DamagableObjHealthControl  _startHealth <= 0.0f {_startHealth}");
				_startHealth = 10.0f;
			}
			this._startHealth = _startHealth;
			this._currHealth = this._startHealth;
		}

		private float _currHealth;

		public event Action<float , DamageType> OnTakeDamage;

		public bool MayDamageMe() => MayChangeHealth();

		public void DamageMe( float damage , DamageType damageType )
		{
			if( MayChangeHealth() == false )
				return;
			if( IsEvasion() )
				return;
			float newHealth = ( _currHealth - damage ).Normalize(0.0f , _startHealth);
			OnTakeDamage?.Invoke(newHealth, damageType);
			SetHealth(newHealth);
		}

		public void HealMe( float healCount )
		{
			if( MayChangeHealth() == false )
				return;
			float newHealth = ( _currHealth + healCount ).Normalize(0.0f , _startHealth);

			SetHealth(newHealth);
		}
		public void HealMePercent( float perc )
		{
			if( MayChangeHealth() == false )
				return;
			float newHealth = ( _currHealth + ( _startHealth * perc ) ).Normalize(0.0f , _startHealth);

			SetHealth(newHealth);
		}

		private void SetHealth( float value )
		{
			_currHealth = value;
			ShowSlider(_currHealth > 0.0f ? 2.0f : 0.5f);
		}

		private HealthSlider _healthSlider;
		private void ShowSlider( float duration )
		{
			_hideTime = Time.time + duration;

			_healthSlider = _healthSlider != null ? _healthSlider : _healthSliderFactory.Get(_sliderTransform);
			_healthSlider.SetValue(_currHealth/ _startHealth);
		}
		private void HideSlider()
		{
			_healthSliderFactory.Return(_healthSlider);
			_healthSlider = null;
		}


		private float _hideTime;
		public void Update()
		{
			if( _healthSlider == null )
				return;
			if( Time.time < _hideTime )
				return;
			HideSlider();
		}
	}
}
