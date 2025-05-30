using System;
using System.Collections;


using UnityEngine;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MobSkinRanged:MobSkinBasic
	{
		private const string _animParName = "intPar";
		private const string _animWalkMultName = "floatPar";
		private enum AnimType
		{
			idle = 0,
			walk = 1,
			attackPistol1 = 2,
			attackPistol2 = 3,
			dead = 4,
			stayPistol = 5
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


		public override void PlayAttack( Action onMakeDamageComplete , Action onAttackFinish )
		{
			if( _state == State.dead )
			{
				return;
			}
			base.PlayAttack(onMakeDamageComplete , onAttackFinish);
			_state = State.attack;
			if( SelfAnimator.GetInteger(_animParName) == (int)AnimType.attackPistol1 )
			{
				SelfAnimator.SetInteger(_animParName , (int)AnimType.attackPistol2);
				return;
			}
			if( SelfAnimator.GetInteger(_animParName) == (int)AnimType.attackPistol2 )
			{
				SelfAnimator.SetInteger(_animParName , (int)AnimType.attackPistol1);
				return;
			}

			SelfAnimator.SetInteger(_animParName , (int)AnimType.attackPistol1);
		}

		public override void PlayDead( Action onDeadAnimComplete )
		{
			base.PlayDead(onDeadAnimComplete);
			SelfAnimator.SetInteger(_animParName , (int)AnimType.dead);
		}

		public void PlayStayPistol()
		{
			SelfAnimator.SetInteger(_animParName , (int)AnimType.stayPistol);
		}
	}
}
