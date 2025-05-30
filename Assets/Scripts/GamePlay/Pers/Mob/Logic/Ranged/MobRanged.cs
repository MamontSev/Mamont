using System;
using System.Collections.Generic;

using Mamont.Data.DataControl.Mob;
using Mamont.Gameplay.Control.Damage;

using Zenject;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MobRanged:MobBasic
	{
		[Inject]
		private void Construct
		(
			IMobRangedDataControl _selfDataControl 
		)
		{
			_basicDataControl = _selfDataControl;
		}

		private void Awake()
		{
			InitName();
			RegisterStates();
			_stateMachine.Enter<MobStartAwaitState>();
		}
		private void Start()
		{
			_stateMachine.Enter<MobIdleWalkingState>();
		}

		private void RegisterStates()
		{
			_stateMachine.Register<MobStartAwaitState>(_stateFactory.Create<MobStartAwaitState>());
			_stateMachine.Register<MobIdleWalkingState>(_stateFactory.Create<MobIdleWalkingState>());
			_stateMachine.Register<IMobOnFoundTargetState>(_stateFactory.Create<RangedFightState>());
			_stateMachine.Register<MobKilledState>(_stateFactory.Create<MobKilledState>());
		}

		private static int _nameCounter = 0;
		private void InitName()
		{
			this.name = "Ranged_" + _nameCounter.ToString();
			_nameCounter++;
		}

	}
}
