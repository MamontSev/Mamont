using Manmont.Tools.StateMashine;

using UnityEngine;

namespace Mamont.Gameplay.Pers.Mob
{
	public class KamikazeBoomedState:IMobState, IEnterState
	{
		private readonly MobBasic _mobBasic;
		public KamikazeBoomedState
		(
			MobBasic _mobBasic
		)
		{
			this._mobBasic = _mobBasic;
		}
		public void Enter()
		{
			Object.Destroy(_mobBasic.gameObject);
		}
	}
}

