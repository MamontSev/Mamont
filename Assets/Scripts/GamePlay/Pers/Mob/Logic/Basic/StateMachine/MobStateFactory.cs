using Zenject;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MobStateFactory
	{
		private readonly DiContainer _diContainer;
		public MobStateFactory( DiContainer _diContainer )
		{
			this._diContainer = _diContainer;
		}

		public T Create<T>() where T : IMobState
		{
			return _diContainer.Instantiate<T>();
		}
	}
}
