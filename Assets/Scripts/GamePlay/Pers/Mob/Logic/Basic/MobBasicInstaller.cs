using UnityEngine;

using Zenject;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MobBasicInstaller:MonoInstaller
	{
		public override void InstallBindings()
		{
			BindMobHouseDoorControl();
			BindMobIdleWalker();
			BindMovableObjSpeedControl();
			BindMobTargetFinder();
			BindMobBasic();
			BindMobStateMachine();
		}
		private void BindMobStateMachine()
		{
			Container.Bind<MobStateMachine>().AsSingle();
			Container.Bind<MobStateFactory>().AsSingle();
		}
		private void BindMobHouseDoorControl()
		{
			Container.Bind<MobHouseDoorControl>().AsSingle();
		}
		private void BindMobIdleWalker()
		{
			Container.Bind<MobIdleWalker>().AsSingle();
		}
		private void BindMovableObjSpeedControl()
		{
			Container.Bind<MovableObjSpeedControl>().AsSingle();
		}
		private void BindMobTargetFinder()
		{
			Container.Bind<MobTargetFinder>().AsSingle();
		}

		[SerializeField]
		private MobBasic _mobBasic;
		private void BindMobBasic()
		{
			Container.Bind<MobBasic>().FromInstance(_mobBasic).AsSingle();
		}


	}
}
