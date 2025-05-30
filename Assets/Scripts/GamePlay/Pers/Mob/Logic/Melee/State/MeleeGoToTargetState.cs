using Mamont.Data.DataControl.Mob;

using Manmont.Tools.StateMashine;

using UnityEngine;
using UnityEngine.AI;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MeleeGoToTargetState:IMobOnFoundTargetState, IEnterState, IUpdateState
	{
		private readonly MobBasic _mobBasic;
		private readonly MobTargetFinder _targetFinder;
		private readonly MobStateMachine _stateMachine;
		private readonly IMobMeleeDataControl _selfDataControl;
		public MeleeGoToTargetState						  
		(
			MobBasic _mobBasic ,
			MobTargetFinder _targetFounder ,
			MobStateMachine _stateMachine ,
			IMobMeleeDataControl _selfDataControl
		)
		{												 			    
			this._mobBasic = _mobBasic;		   
			this._targetFinder = _targetFounder;
			this._stateMachine = _stateMachine;
			this._selfDataControl = _selfDataControl;

			_navMeshAgent = _mobBasic.NavMeshAgent;
		}

		private NavMeshAgent _navMeshAgent;
		private int SelfLevel => _mobBasic.SelfLevel;			 


		public void Enter()
		{
			_timeToGo = 0.0f;
			_navMeshAgent.enabled = true;
			_navMeshAgent.speed = _selfDataControl.WalkSpeed(SelfLevel);
		}

		private float _timeToGo = 0;
		public void Update()
		{
			if( Time.time < _timeToGo )
			{
				return;
			}
			_timeToGo = Time.time + 0.2f;

			if( _targetFinder.FindTarget() == null )
			{
				_stateMachine.Enter<MobIdleWalkingState>();
				return;
			}

			_navMeshAgent.destination = _targetFinder.CurrTarget.Position;
			float dist = Vector3.Distance(_mobBasic.InContainer.position , _targetFinder.CurrTarget.Position);
			if( dist < _selfDataControl.AttackDistance(SelfLevel) )
			{
				_stateMachine.Enter<MeleeFightState>();
			}
		}

		
	}
}
