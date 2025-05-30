using UnityEngine;

namespace Manmont.Tools
{
	public class DestroyOnAnimationEvent:MonoBehaviour
	{
		public void OnAnimationComplete()
		{
			Destroy(gameObject);
		}
	}
}
