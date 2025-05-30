using Mamont.Data.DataControl.Mob;
using Mamont.Gameplay.Control.Damage;

using Manmont.Tools.StateMashine;

using UnityEngine;
using UnityEngine.AI;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MeleeFightState:IMobState, IEnterState, IUpdateState
	{
		private readonly MobBasic _mobBasic;
		private readonly MobTargetFinder _targetFinder;
		private readonly MobStateMachine _stateMachine;
		private readonly IMobMeleeDataControl _selfDataControl;
		private readonly IDamageService _damageService;
		public MeleeFightState
		(
			MobBasic _mobBasic ,
			MobTargetFinder _targetFinder ,
			MobStateMachine _stateMachine ,
			IMobMeleeDataControl _selfDataControl ,
			IDamageService _damageService
		)
		{
			this._mobBasic = _mobBasic;
			this._targetFinder = _targetFinder;
			this._stateMachine = _stateMachine;
			this._selfDataControl = _selfDataControl;
			this._damageService = _damageService;

			_navMeshAgent = _mobBasic.NavMeshAgent;
		}

		private NavMeshAgent _navMeshAgent;

		private int SelfLevel => _mobBasic.SelfLevel;

		public void Enter()
		{
			_timeRunFight = 0.0f;
			_navMeshAgent.enabled = true;
			_navMeshAgent.speed = 0.0f;
		}

		private float _timeRunFight = 0;
		public void Update()
		{
			if( _targetFinder.IsCorrectTarget() )
			{
				_mobBasic.InContainer.LookAt(_targetFinder.CurrTarget.Position);
			}
			else
			{
				_stateMachine.Enter<MobIdleWalkingState>();
				return;
			}

			if( Time.time < _timeRunFight )
				return;

			if( IsCorrectTargetAndDist() == false )
			{
				_stateMachine.Enter<MobIdleWalkingState>();
				return;
			}



			_timeRunFight = _selfDataControl.BeatInterval(SelfLevel) + Time.time + 2.0f;


			_mobBasic.Skin.PlayAttack(makeAttack , onFinishAttack);

			void makeAttack()
			{
				if( IsCorrectTargetAndDist() == false )
				{
					_stateMachine.Enter<MobIdleWalkingState>();
					return;
				}
				_damageService.MakeDamage<IDamagableByMob>(
				_targetFinder.CurrTarget ,
				_selfDataControl.Damage(SelfLevel) ,
			   _selfDataControl.GroupAttack(SelfLevel) ,
			   _selfDataControl.GroupAttackRadius(SelfLevel) ,
			   DamageType.Melee);

				_timeRunFight = _selfDataControl.BeatInterval(SelfLevel) + Time.time;
			}

			void onFinishAttack()
			{
				if( _stateMachine.CurrState is MeleeFightState )
				{
					_mobBasic.Skin.PlayIdleWalk();
				}
			}
		}

		private bool IsCorrectTargetAndDist()
		{
			if( _targetFinder.IsCorrectTarget() == false )
				return false;

			float dist = Vector3.Distance(_mobBasic.InContainer.position, _targetFinder.CurrTarget.Position);

			return dist <= _selfDataControl.AttackDistance(SelfLevel);

		}
	}
}
