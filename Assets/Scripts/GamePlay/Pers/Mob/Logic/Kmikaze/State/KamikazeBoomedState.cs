using Mamont.Gameplay.Control.Mob;

using Manmont.Tools.StateMashine;

namespace Mamont.Gameplay.Pers.Mob
{
	public class KamikazeBoomedState:IMobState, IEnterState
	{
		private readonly MobBasic _mobBasic;
		private readonly IMobFactory _mobFactory;
		public KamikazeBoomedState
		(
			MobBasic _mobBasic,
			IMobFactory _mobFactory
		)
		{
			this._mobBasic = _mobBasic;
			this._mobFactory = _mobFactory;
		}
		public void Enter()
		{
			_mobFactory.OnKilled(_mobBasic , false);
		}
	}
}

