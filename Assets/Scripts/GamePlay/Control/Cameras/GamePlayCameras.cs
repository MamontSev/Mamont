using Mamont.Gameplay.Control.LevelState;

using UnityEngine;

using Zenject;

namespace Mamont.Gameplay.Control.Cameras
{
	public class GamePlayCameras:MonoBehaviour, IGamePlayCameras
	{
		private ILevelStateControl _levelStateControl;

		[Inject]
		private void Construct
		(
			  ILevelStateControl _levelStateControl
		)
		{
			this._levelStateControl = _levelStateControl;
		}
		private void Start()
		{
			Init();
		}

		private void Update()
		{
			if( _levelStateControl.IsPlaying == false )
			{
				return;
			}
			SetDisabledCameraSize();
		}

		private void Init(  )
		{
			Camera3D.cullingMask = _cameraLayerMask;

			SetDisabledCameraSize();

			_dragDisabledCamera.transform.SetPositionAndRotation(Camera3D.transform.position , Camera3D.transform.rotation);

			GameObject _temp = GameObject.Instantiate(new GameObject());
			_temp.name = "Temp";
			_temp.transform.position = Camera3D.transform.position + Camera3D.transform.forward * 10.0f;
			_temp.transform.LookAt(Camera3D.transform);
			_toMainCameraNormalizedRotation = _temp.transform.rotation;
		}

		[SerializeField]
		private Camera _UICamera2d;

		public void SetEnableUiCamera2d( bool state )
		{
			_UICamera2d.enabled = state;
		}
		public void SetEnableCamera3d( bool state )
		{
			Camera3D.enabled = state;
		}

		[SerializeField]
		private LayerMask _cameraLayerMask;

		[SerializeField]
		private Camera _dragDisabledCamera;
		public Camera DragDisabledCamera => _dragDisabledCamera;

		[SerializeField] private Camera _camera3d;
		public Camera Camera3D => _camera3d;

		[SerializeField]
		private Transform _levelCameraContainer;
		public Transform LevelCameraContainer => _levelCameraContainer;



		private void SetDisabledCameraSize()
		{
			if( Camera3D.orthographic == true )
			{
				_dragDisabledCamera.orthographic = true;
				_dragDisabledCamera.orthographicSize = Camera3D.orthographicSize;
			}
			else
			{
				_dragDisabledCamera.orthographic = false;
				_dragDisabledCamera.fieldOfView = Camera3D.fieldOfView;
			}
		}

		private float _minSize = 7.0f;
		private float _maxSize = 50.0f;
		public void ChangeScaleMainCamera( float delta )
		{
			Camera3D.orthographicSize -= delta / 20.0f;
			if( Camera3D.orthographicSize > _maxSize )
			{
				Camera3D.orthographicSize = _maxSize;
			}
			if( Camera3D.orthographicSize < _minSize )
			{
				Camera3D.orthographicSize = _minSize;
			}
		}

		private Quaternion _toMainCameraNormalizedRotation = Quaternion.identity;
		public Quaternion ToMainCameraNormalizedRotation => _toMainCameraNormalizedRotation;

	}
}
