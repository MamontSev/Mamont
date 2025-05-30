using System;
using System.Collections;

using Mamont.Gameplay.Control.Damage;

using UnityEngine;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MobSkinKamikaze:MobSkinBasic
	{
		[Header("Эффект взрыва - тип BoomEffect")]
		[SerializeField]
		private BoomEffect _boomEffect;
		public BoomEffect BoomEffect => _boomEffect;


		private const string _animParName = "intPar";
		private const string _animWalkMultName = "floatPar";
		private enum AnimType
		{
			idle = 0,
			walk = 1,
			boomPrepare = 2,
			dead = 3
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


		
		private Action onPrepareBoomComplete;
		public void PlayBoomPrepare( Action onPrepareBoomComplete )
		{
			if( _state == State.dead )
			{
				return;
			}
			this.onPrepareBoomComplete = onPrepareBoomComplete;

			_state = State.attack;

			SelfAnimator.SetInteger(_animParName , (int)AnimType.boomPrepare);
		}
		public void BoomPrepareCompleteAnimEvent()
		{
			onPrepareBoomComplete?.Invoke();
		}




		public override void PlayDead( Action onDeadAnimComplete )
		{
			base.PlayDead(onDeadAnimComplete);
			SelfAnimator.SetInteger(_animParName , (int)AnimType.dead);
		}
	}
}

