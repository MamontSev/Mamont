using System;

using UnityEngine;

namespace Mamont.Gameplay.Pers.Mob
{
	[RequireComponent(typeof(Animator))]
	public abstract class MobSkinBasic:MonoBehaviour
	{
		[Header("Чтобы скорость ходьбы подстроить")]
		[SerializeField]
		protected float _basicWalkSpeed = 2.7f;

		[Header("Радиус нивмгации - зависит от размера скина")]
		[SerializeField]
		protected float _navigationRadius = 0.3f;
		public float NavigationRadius => _navigationRadius;

		[SerializeField]
		private Transform _healthIndicatorParent;
		public Transform HealthIndicatorParent => _healthIndicatorParent;

		protected Animator SelfAnimator => GetComponent<Animator>();

		protected State _state = State.idleWalk;
		protected enum State
		{
			idleWalk = 0,
			attack = 1,
			dead = 2
		}


		public abstract void UpdateMe(float currSpeed);

		protected Action onMakeDamageComplete;
		protected Action onAttackFinish;
		public virtual void PlayAttack( Action onMakeDamageComplete, Action onAttackFinish )
		{
			this.onMakeDamageComplete = onMakeDamageComplete;
			this.onAttackFinish = onAttackFinish;
		}
		public void OnMakeDamage()
		{
			onMakeDamageComplete?.Invoke();
		}
		public void OnAttackFinish()
		{
			onAttackFinish?.Invoke();
		}
	

		
		
		protected Action onDeadAnimComplete;
		public virtual void PlayDead( Action onDeadAnimComplete )
		{
			_state = State.dead;
			this.onDeadAnimComplete = onDeadAnimComplete;
		}
		public void OnDeadFinish()
		{
			onDeadAnimComplete?.Invoke();
		}


		public void PlayIdleWalk()
		{
			if( _state == State.dead )
			{
				return;
			}
			_state = State.idleWalk;
		}

	

		
	}
}
