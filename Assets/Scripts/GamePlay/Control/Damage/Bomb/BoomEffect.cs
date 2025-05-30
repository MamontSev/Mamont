

using UnityEngine;

namespace Mamont.Gameplay.Control.Damage
{
	public class BoomEffect:MonoBehaviour
	{

		private void Start()
		{
			Destroy(gameObject,1.5f);
		}
	}
}
