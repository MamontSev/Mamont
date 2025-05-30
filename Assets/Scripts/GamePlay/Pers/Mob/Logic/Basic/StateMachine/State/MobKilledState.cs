using Manmont.Tools.StateMashine;

using UnityEngine;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MobKilledState:IMobState, IEnterState
	{
		private readonly MobBasic _mobBasic;

		public MobKilledState
		(
			MobBasic _mobBasic 
		)
		{
			this._mobBasic = _mobBasic;

		}
		public void Enter()
		{
			_mobBasic.NavMeshAgent.enabled = false;
			_mobBasic.Skin.PlayDead(() =>
			{
				Object.Destroy(_mobBasic.gameObject);
			});
		}
	}
}
