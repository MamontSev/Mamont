using Manmont.Tools.StateMashine;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MobStartAwaitState:IMobState,IEnterState
	{
		private readonly MobBasic _mobBasic;
		public MobStartAwaitState
		(
			MobBasic _mobBasic
		)
		{
			this._mobBasic = _mobBasic;
		}

		public void Enter()
		{
			_mobBasic.NavMeshAgent.enabled = false;
		}
	}
}
