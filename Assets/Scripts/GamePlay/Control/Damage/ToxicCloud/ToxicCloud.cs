using System;

using UnityEngine;
namespace Mamont.Gameplay.Control.Damage
{
	public class ToxicCloud:MonoBehaviour
	{
		private const float _timeStep = 0.5f;

		public bool IsInit { get; private set; } = false;

		private float damagePerSecond;
		private float duration;
		private Action<Vector3 , float> MakeDamage;

		public void Init( float damagePerSecond , float duration , Action<Vector3 , float> MakeDamage )
		{
			this.damagePerSecond = damagePerSecond;
			this.duration = duration;
			this.MakeDamage = MakeDamage;
			_timeLeft = 0.0f;
			_nextDamageTime = 0.0f;
			IsInit = true;
		}


		private float _nextDamageTime = 0.0f;

		private float _timeLeft = 0.0f;
		public void UpdateDamage()
		{
			if( IsInit == false )
			{
				return;
			}
			_timeLeft += Time.deltaTime;
			if( _timeLeft < _nextDamageTime )
			{
				return;
			}
			_nextDamageTime = _timeLeft + _timeStep;
			MakeDamage(transform.position , damagePerSecond * _timeStep);

			if( _timeLeft > duration )
			{
				IsInit = false;
				//Deactivate(this);
			}
		}
	}
}
