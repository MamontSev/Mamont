using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Manmont.Tools
{
	public class DestroyOnDelay:MonoBehaviour
	{
		[SerializeField]
		private float _delayToDestroy = 1.0f;
		private void Start()
		{
			Destroy(gameObject , _delayToDestroy);
		}

	}
}
