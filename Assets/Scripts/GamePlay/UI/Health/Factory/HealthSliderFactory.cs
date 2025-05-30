using System.Collections.Generic;

using UnityEngine;

using Zenject;

namespace Mamont.Gameplay.UI.Health
{
	public class HealthSliderFactory:MonoBehaviour, IHealthSliderFactory
	{
		private DiContainer _diContainer;
		[Inject]
		private void Construct
		(
			DiContainer _diContainer
		)
		{
			this._diContainer = _diContainer;
		}
		private static int index = 0;
		[SerializeField]
		private HealthSlider _healthSliderPrefab;

		private readonly Queue<HealthSlider> _pool = new();

		public HealthSlider Get( Transform _parentTransform )
		{
			HealthSlider obj;
			if( _pool.Count > 0 )
			{
				
				obj = _pool.Dequeue();
				obj.gameObject.SetActive(true);
			}
			else
			{
				obj = _diContainer.InstantiatePrefabForComponent<HealthSlider>(_healthSliderPrefab.gameObject);
				_diContainer.InjectGameObject(obj.gameObject);

				obj.name = $"Slider {index}";
				index++;
			}
			obj.transform.SetParent(_parentTransform);
			obj.transform.localPosition = Vector3.zero;
			return obj;
		}

		public void Return( HealthSlider obj )
		{
			obj.transform.SetParent(this.transform);
			obj.transform.localPosition = Vector3.zero;
			obj.gameObject.SetActive(false);
			_pool.Enqueue(obj);
		}


	}
}
