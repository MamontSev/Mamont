using UnityEngine;

using Zenject;

namespace Mamont.Gameplay.Control.Damage
{
	public interface IBombFactory
	{
		ThrowBomb CreateBomb( ThrowBomb prefab , Vector3 position , Quaternion rotation );
	}
	public class BombFactory:MonoBehaviour, IBombFactory
	{
		private DiContainer _diContainer;

		[Inject]
		private void Construct( DiContainer _diContainer )
		{
			this._diContainer = _diContainer;
		}

		[SerializeField]
		private Transform _parentTransform;

		public ThrowBomb CreateBomb( ThrowBomb prefab , Vector3 position , Quaternion rotation )
		{
			return _diContainer.InstantiatePrefabForComponent<ThrowBomb>(prefab.gameObject, position, rotation, _parentTransform);
		}
	}
}
