using Mamont.Gameplay.Control.Cameras;

using Manmont.Tools;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Mamont.Gameplay.UI.Health
{
	public class HealthSlider:MonoBehaviour
	{
		private IGamePlayCameras _gamePlayCameras;

		[Inject]
		private void Construct( IGamePlayCameras _gamePlayCameras )
		{
			this._gamePlayCameras = _gamePlayCameras;
		}

		private Quaternion LoockRot => _gamePlayCameras.ToMainCameraNormalizedRotation;

		[SerializeField]
		private Slider _slider;

		
		public void SetValue( float value )
		{
			value.Normalize(0.0f,1.0f);
			_slider.value = value;
		}
		private void Update()
		{
			transform.rotation = LoockRot;
		}

	}
}
