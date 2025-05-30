using System;

using Mamont.Gameplay.Control.Damage;

using UnityEngine;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MobSkinGrenadier:MobSkinBasic
	{

		private const string _animParName = "intPar";
		private const string _animWalkMultName = "floatPar";
		private enum AnimType
		{
			idle = 0,
			walk = 1,
			throw1 = 2,
			throw2 = 3,
			dead = 4
		}

		public override void UpdateMe( float currSpeed )
		{
			if( _state == State.idleWalk )
			{
				if( currSpeed < 0.05f )
				{
					if( SelfAnimator.GetInteger(_animParName) != (int)AnimType.idle )
					{
						SelfAnimator.SetInteger(_animParName , (int)AnimType.idle);
					}
				}
				else
				{
					if( SelfAnimator.GetInteger(_animParName) != (int)AnimType.walk )
					{
						SelfAnimator.SetInteger(_animParName , (int)AnimType.walk);
					}
					SelfAnimator.SetFloat(_animWalkMultName , currSpeed / _basicWalkSpeed);
				}
			}
		}


		public override void PlayAttack( Action onMakeDamageComplete, Action onAttackFinish )
		{
			if( _state == State.dead )
			{
				return;
			}
			base.PlayAttack(onMakeDamageComplete, onAttackFinish);
			_state = State.attack;
			if( SelfAnimator.GetInteger(_animParName) == (int)AnimType.throw1 )
			{
				SelfAnimator.SetInteger(_animParName , (int)AnimType.throw2);
				return;
			}
			if( SelfAnimator.GetInteger(_animParName) == (int)AnimType.throw2 )
			{
				SelfAnimator.SetInteger(_animParName , (int)AnimType.throw1);
				return;
			}

			SelfAnimator.SetInteger(_animParName , (int)AnimType.throw1);
		}

		public override void PlayDead( Action onDeadAnimComplete )
		{
			base.PlayDead(onDeadAnimComplete);
			SelfAnimator.SetInteger(_animParName , (int)AnimType.dead);
		}
		[Header("Точка откуда кидается граната")]
		[SerializeField] private Transform _throwTransform;
		public Transform ThrowTransform => _throwTransform;
		[Header("Угол под которым кидается граната")]
		[SerializeField] private float _throwAngle = 30.0f;
		public float ThrowAngle => _throwAngle;
		[Header("Префаб гранаты - тип ThrowBomb")]
		[SerializeField] private ThrowBomb _bombPrefab;
		public ThrowBomb BombPrefab => _bombPrefab;
	}
}

