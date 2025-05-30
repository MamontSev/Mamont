using Mamont.Data.DataControl.Mob;
using Mamont.Gameplay.Control.Damage;

using Manmont.Tools.StateMashine;

using UnityEngine;

namespace Mamont.Gameplay.Pers.Mob
{
	public class RangedFightState:IMobOnFoundTargetState, IEnterState, IUpdateState
	{
		private readonly MobBasic _mobBasic;
		private readonly MobTargetFinder _targetFinder;
		private readonly MobStateMachine _stateMachine;
		private readonly IDamageService _damageService;
		private readonly IMobRangedDataControl _selfDataControl;
		public RangedFightState
		(
			MobBasic _mobBasic ,
			MobTargetFinder _targetFounder ,
			MobStateMachine _stateMachine ,
			IDamageService _damageService ,
			IMobRangedDataControl _selfDataControl
		)
		{
			this._mobBasic = _mobBasic;
			this._targetFinder = _targetFounder;
			this._stateMachine = _stateMachine;
			this._damageService = _damageService;
			this._selfDataControl = _selfDataControl;
		}
		private int SelfLevel => _mobBasic.SelfLevel;
		private MobSkinRanged SelfSkin => _mobBasic.Skin as MobSkinRanged;
		public void Enter()
		{
			_mobBasic.NavMeshAgent.speed = 0.0f;
			_timeRunFight = 0.0f;
		}

		private float _timeRunFight = 0.0f;
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

			_mobBasic.InContainer.LookAt(_targetFinder.CurrTarget.Position);

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
			   DamageType.Ranged);
				_timeRunFight = _selfDataControl.BeatInterval(SelfLevel) + Time.time;
			}

			void onFinishAttack()
			{
				if( _stateMachine.CurrState is RangedFightState )
				{
					SelfSkin.PlayStayPistol();
				}
			}
		}

		private bool IsCorrectTargetAndDist()
		{
			if( _targetFinder.IsCorrectTarget() == false )
				return false;

			float dist = Vector3.Distance(_mobBasic.InContainer.position , _targetFinder.CurrTarget.Position);
			
			float radius = _mobBasic.AgressionRadius;
			if( dist > radius )
				return false;

			float minRadius = _mobBasic.MinAgressionRadius;
			if( dist < minRadius )
				return false;

			return true;
		}
	}

}
