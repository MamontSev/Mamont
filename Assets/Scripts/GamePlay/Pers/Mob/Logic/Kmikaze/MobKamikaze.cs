using System;
using System.Collections.Generic;

using Mamont.Data.DataControl.Mob;
using Mamont.Gameplay.Control.Damage;

using UnityEngine;

using Zenject;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MobKamikaze:MobBasic
	{
		[Inject]
		private void Construct
		(
			IMobKamikazeDataControl _selfDataControl 
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
			_stateMachine.Register<IMobOnFoundTargetState>(_stateFactory.Create<KamikazeGoToTargetState>());
			_stateMachine.Register<KamikazeBoomProcessState>(_stateFactory.Create<KamikazeBoomProcessState>());
			_stateMachine.Register<KamikazeBoomedState>(_stateFactory.Create<KamikazeBoomedState>());
			_stateMachine.Register<MobKilledState>(_stateFactory.Create<MobKilledState>());
		}

		private static int _nameCounter = 0;
		private void InitName()
		{
			this.name = "Kamikaze_" + _nameCounter.ToString();
			_nameCounter++;
		}

		protected override bool MayDamageMe()
		{
			if( base.MayDamageMe() == false )
				return false;

			return _stateMachine.CurrState.GetType() != typeof(KamikazeBoomedState);
		}



		public void CreateBoomEffect(GameObject prefab,Vector3 pos)
		{
			GameObject effect = GameObject.Instantiate(prefab);
			effect.transform.position = pos;
		}

		

	

	}
}

