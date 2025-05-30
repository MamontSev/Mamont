using Manmont.Tools.StateMashine;

using UnityEngine;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MobIdleWalkingState:IMobState, IEnterState, IUpdateState
	{
		private readonly MobBasic _mobBasic;
		private readonly MobIdleWalker _mobIdleWalker;
		private readonly MobTargetFinder _targetFinder;
		private readonly MobStateMachine _stateMachine;
		public MobIdleWalkingState
		(
			MobBasic _mobBasic ,
			MobIdleWalker _mobIdleWalker ,
			MobTargetFinder _targetFounder ,
			MobStateMachine _stateMachine
		)
		{
			this._mobBasic = _mobBasic;
			this._mobIdleWalker = _mobIdleWalker;
			this._targetFinder = _targetFounder;
			this._stateMachine = _stateMachine;
		}


		public void Enter()
		{
			_mobBasic.NavMeshAgent.enabled = true;
			_mobBasic.NavMeshAgent.speed = 1.5f;

			_mobIdleWalker.Init(_mobBasic.InContainer.position);

			_mobBasic.Skin.PlayIdleWalk();
		}

		public void Update()
		{												   
			if( _mobBasic.IsInHouse == false )
			{
				FindTarget();
			}
			_mobIdleWalker.Update();
		}

		private float _timeFindTarget = 0;
		private void FindTarget()
		{
			if( Time.time < _timeFindTarget )
			{
				return;
			}
			_timeFindTarget = Time.time + 0.2f;

			if( _targetFinder.FindTarget() != null )
			{
				_stateMachine.Enter<IMobOnFoundTargetState>();
			}
		}
	}
}
