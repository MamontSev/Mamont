using System.Collections.Generic;

using Mamont.Gameplay.Control.LevelState;

using UnityEngine;

using Zenject;

namespace Mamont.Gameplay.Control.Damage
{
	public interface IToxicCloudControl
	{
		void Create( Vector3 pos , float damagePerSec , float duration );
	}
	public class ToxicCloudControl:MonoBehaviour, IToxicCloudControl
	{
		private IDamageService _damageControl;
		private ILevelStateControl _levelStateControl;

		[Inject]
		private void Constuct
		(
			IDamageService _damageControl,
			ILevelStateControl _levelStateControl
		)
		{
			this._damageControl = _damageControl;
			this._levelStateControl = _levelStateControl;
		}


		[SerializeField]
		private Transform Container;
		[SerializeField]
		private ToxicCloud _toxicCloudPrefab;

		private readonly Queue<ToxicCloud> _pool = new();

		private  List<ToxicCloud> _cloudList = new();

		public void Create( Vector3 pos , float damagePerSec , float duration )
		{
			ToxicCloud obj;
			if( _pool.Count > 0 )
			{
				obj = _pool.Dequeue();
			}
			else
			{
				obj = GameObject.Instantiate(_toxicCloudPrefab.gameObject , Container).GetComponent<ToxicCloud>();
			}
			obj.gameObject.SetActive(true);
			obj.transform.position = pos;
			obj.Init(damagePerSec , duration , Damage );
			_cloudList.Add(obj);
		}

		private void Damage( Vector3 pos , float damage )
		{
			_damageControl.DamageByToxicCloud(pos , damage);
		}

		private void Update()
		{
			if( _levelStateControl.IsPlaying == false )
			{
				return;
			}
			if( _cloudList.Count == 0 )
			{
				return;
			}

			for( int i = _cloudList.Count - 1; i >= 0; i-- )
			{
				_cloudList[i].UpdateDamage();
				if( _cloudList[i].IsInit == false )
				{
					_pool.Enqueue(_cloudList[i]);
					_cloudList[i].gameObject.SetActive(false);
					_cloudList.RemoveAt(i);
				}
					
			}


		}
	}
}
