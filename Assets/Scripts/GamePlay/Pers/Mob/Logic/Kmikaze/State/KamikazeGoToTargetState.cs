using Mamont.Data.DataControl.Mob;

using Manmont.Tools.StateMashine;

using UnityEngine;
using UnityEngine.AI;

namespace Mamont.Gameplay.Pers.Mob
{
	public class KamikazeGoToTargetState:IMobOnFoundTargetState, IEnterState, IUpdateState
	{
		private readonly MobBasic _mobBasic;
		private readonly MobTargetFinder _targetFinder;
		private readonly MobStateMachine _stateMachine;
		private readonly IMobKamikazeDataControl _selfDataControl;
		public KamikazeGoToTargetState
		(
			MobBasic _mobBasic ,
			MobTargetFinder _targetFinder ,
			MobStateMachine _stateMachine ,
			IMobKamikazeDataControl _selfDataControl
		)
		{
			this._mobBasic = _mobBasic;
			this._targetFinder = _targetFinder;
			this._stateMachine = _stateMachine;
			this._selfDataControl = _selfDataControl;
		}


		public void Enter()
		{
			_timeRunTarget = 0.0f;
			_mobBasic.NavMeshAgent.enabled = true;
			_mobBasic.NavMeshAgent.speed = _selfDataControl.WalkSpeed(_mobBasic.SelfLevel);
		}

		private float _timeRunTarget = 0;
		public void Update()
		{
			if( Time.time < _timeRunTarget )
			{
				return;
			}
			_timeRunTarget = Time.time + 0.2f;

			_targetFinder.FindTarget();

			if( _targetFinder.CurrTarget == null )
			{
				_stateMachine.Enter<MobIdleWalkingState>();
				return;
			}
			if( _targetFinder.CurrTarget.HealthControl.MayDamageMe() == false )
			{
				_stateMachine.Enter<MobIdleWalkingState>();
				return;
			}

			_mobBasic.NavMeshAgent.destination = _targetFinder.CurrTarget.Position;
			float dist = Vector3.Distance(_mobBasic.InContainer.position , _targetFinder.CurrTarget.Position);
			if( dist < _selfDataControl.AttackDistance(_mobBasic.SelfLevel) )
			{
				_stateMachine.Enter<KamikazeBoomProcessState>();
			}
		}

		
	}
}

