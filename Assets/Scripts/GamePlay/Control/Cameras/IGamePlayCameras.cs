using UnityEngine;

namespace Mamont.Gameplay.Control.Cameras
{
	public interface IGamePlayCameras
	{
		void SetEnableUiCamera2d( bool state );
		void SetEnableCamera3d( bool state );

		Camera Camera3D
		{
			get;
		}

		Transform LevelCameraContainer
		{
			get;
		}

		Camera DragDisabledCamera
		{
			get;
		}
		Quaternion ToMainCameraNormalizedRotation
		{
			get;
		}

		void ChangeScaleMainCamera( float delta );
	}
}
