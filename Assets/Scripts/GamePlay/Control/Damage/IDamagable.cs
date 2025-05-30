using UnityEngine;

namespace Mamont.Gameplay.Control.Damage
{
	public interface IDamagable
	{
		IDamagableObjHealthControl HealthControl
		{
			get;
		}

		Vector3 Position
		{
			get;
		}

		string Name
		{
			get;
		}

	}
}
