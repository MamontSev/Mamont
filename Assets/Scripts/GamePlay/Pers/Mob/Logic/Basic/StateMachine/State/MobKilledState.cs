using Mamont.Gameplay.Control.Mob;

using Manmont.Tools.StateMashine;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MobKilledState:IMobState, IEnterState
	{
		private readonly MobBasic _mobBasic;
		private readonly IMobFactory _mobFactory;

		public MobKilledState
		(
			MobBasic _mobBasic ,
			IMobFactory _mobFactory
		)
		{
			this._mobBasic = _mobBasic;
			this._mobFactory = _mobFactory;

		}
		public void Enter()
		{
			_mobBasic.NavMeshAgent.enabled = false;
			_mobBasic.Skin.PlayDead(() =>
			{
				_mobFactory.OnKilled(_mobBasic , true);
			});
		}
	}
}
