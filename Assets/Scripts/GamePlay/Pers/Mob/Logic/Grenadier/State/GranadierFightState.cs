using System;

using Mamont.Data.DataControl.Mob;
using Mamont.Gameplay.Control.Damage;

using Manmont.Tools.StateMashine;

using UnityEngine;

namespace Mamont.Gameplay.Pers.Mob
{
	public class GranadierFightState:IMobOnFoundTargetState, IEnterState, IUpdateState
	{
		private readonly MobBasic _mobBasic;
		private readonly MobTargetFinder _targetFinder;
		private readonly MobStateMachine _stateMachine;
		private readonly IMobGrenadierDataControl _selfDataControl;
		private readonly IBombFactory _bombFactory;
		public GranadierFightState
		(
			MobBasic _mobBasic ,
			MobTargetFinder _targetFounder ,
			MobStateMachine _stateMachine ,
			IMobGrenadierDataControl _selfDataControl,
			IBombFactory _bombFactory
		)
		{
			this._mobBasic = _mobBasic;
			this._targetFinder = _targetFounder;
			this._stateMachine = _stateMachine;
			this._selfDataControl = _selfDataControl;
			this._bombFactory = _bombFactory;
		}
		private MobSkinGrenadier SelfSkin => _mobBasic.Skin as MobSkinGrenadier;
		private MobGrenadier Self => _mobBasic as MobGrenadier;

		public void Enter()
		{
			_mobBasic.NavMeshAgent.speed = 0.0f;
			_timeRunFight = 0;
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

			_timeRunFight = _selfDataControl.BeatInterval(_mobBasic.SelfLevel) + Time.time + 3.0f;


			_mobBasic.Skin.PlayAttack(makeAttack , onFinishAttack);

			void makeAttack()
			{
				if( IsCorrectTargetAndDist() == false )
				{
					_stateMachine.Enter<MobIdleWalkingState>();
					return;
				}

				Vector3 _tPos = _targetFinder.CurrTarget.Position;

				if( _targetFinder.CurrTarget is IMovableTarget movableTarget )
				{
					float dist = Vector3.Distance(_mobBasic.InContainer.position , _targetFinder.CurrTarget.Position);
					float targetSpeed = movableTarget.CurrSpeed.magnitude;
					float flightTime = dist / 10.0f;
					_tPos = _targetFinder.CurrTarget.Position + movableTarget.CurrSpeed * flightTime;

				}
				//_temp.position = _tPos;

				_mobBasic.InContainer.LookAt(_tPos);
				Self.ThrowTransform.position = SelfSkin.ThrowTransform.position;
				Self.ThrowTransform.localEulerAngles = new Vector3(-SelfSkin.ThrowAngle , 0 , 0);


				float angleRad = SelfSkin.ThrowAngle * (float)Math.PI / 180.0f;
				Vector3 fromTo = _tPos - Self.ThrowTransform.position;
				Vector3 fromToXZ = new Vector3(fromTo.x , 0f , fromTo.z);
				float x = fromToXZ.magnitude;
				float y = fromTo.y;
				double v2 = ( Physics.gravity.y * x * x ) / ( 2 * ( y - Math.Tan(angleRad) * x ) * Math.Pow(Math.Cos(angleRad) , 2) );
				float v = (float)Math.Sqrt(Math.Abs(v2));

				ThrowBomb bomb = _bombFactory.CreateBomb(SelfSkin.BombPrefab , Self.ThrowTransform.position , Quaternion.identity);

				bomb.Init
				(
				  _selfDataControl.Damage(_mobBasic.SelfLevel) ,
				  _selfDataControl.AttackRadius(_mobBasic.SelfLevel) ,
				  _selfDataControl.AttackCenter(_mobBasic.SelfLevel) ,
				  _selfDataControl.AttackEdge(_mobBasic.SelfLevel) ,
				  Self.ThrowTransform.transform.forward * v
				);
				_timeRunFight = _selfDataControl.BeatInterval(_mobBasic.SelfLevel) + Time.time;
			}

			void onFinishAttack()
			{
				if( _stateMachine.CurrState is GranadierFightState )
				{
					_mobBasic.Skin.PlayIdleWalk();
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
