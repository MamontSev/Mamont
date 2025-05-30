using Mamont.Data.DataControl.Mob;

using UnityEngine;

using Zenject;

namespace Mamont.Gameplay.Pers.Mob
{
	public sealed class MobMelee:MobBasic
	{
		[Inject]
		private void Construct
		(
			IMobMeleeDataControl _selfDataControl
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
			_stateMachine.Register<IMobOnFoundTargetState>(_stateFactory.Create<MeleeGoToTargetState>());
			_stateMachine.Register<MeleeFightState>(_stateFactory.Create<MeleeFightState>());
			_stateMachine.Register<MobKilledState>(_stateFactory.Create<MobKilledState>());
		}

		private static int _nameCounter = 0;
		private void InitName()
		{
			this.name = "Melee_" + _nameCounter.ToString();
			_nameCounter++;
		}

	}
}
