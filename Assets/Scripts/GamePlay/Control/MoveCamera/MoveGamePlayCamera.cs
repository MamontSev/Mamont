using Mamont.Gameplay.Control.Cameras;
using Mamont.Gameplay.Control.LevelState;
using Mamont.Gameplay.Level.Border;

using UnityEngine;
using UnityEngine.EventSystems;

using Zenject;

namespace Mamont.Gameplay.Control.MoveCamera
{
	public class MoveGamePlayCamera:MonoBehaviour, IMoveGamePlayCamera
	{
		private ILevelStateControl _levelStateControl;
		private IGamePlayCameras _gamePlayCameras;
		private LevelCameraBorder _cameraBorders;

		[Inject]
		private void Construct
		(
			  ILevelStateControl _levelStateControl ,
			  IGamePlayCameras _gamePlayCameras ,
			  LevelCameraBorder _cameraBorders
		)
		{
			this._levelStateControl = _levelStateControl;
			this._gamePlayCameras = _gamePlayCameras;
			this._cameraBorders = _cameraBorders;
		}


		private Transform CameraContainerTransform => _gamePlayCameras.LevelCameraContainer;
		private Camera DragDisabledCamera => _gamePlayCameras.DragDisabledCamera;
		private Camera MainCamera => _gamePlayCameras.Camera3D;

		private void Start()
		{
			_groundPlane = new Plane(Vector3.up , 0.0f);
			Init();
		}
		private void Update()
		{
			if( _levelStateControl.IsPlaying == false )
			{
				return;
			}
			UpdateTouch1();

		}


		[SerializeField]
		private GameObject _dragDisabledObj;

		[SerializeField]
		private Transform _borderCont;
		[SerializeField]
		private Transform _leftViewObj;
		[SerializeField]
		private Transform _rightViewObj;
		[SerializeField]
		private Transform _topViewObj;
		[SerializeField]
		private Transform _bottomViewObj;


		private void Init(  )
		{
			_lerpPos = CameraContainerTransform.position;
			_lerpPos.y = 0.0f;

			_borderCont.parent = CameraContainerTransform;
		}

		private int _fingerIdMove1 = -1;
		private int _fingerIdMove2 = -1;
		private Vector2 _fingerPos1 = Vector2.zero;
		private Vector2 _fingerPos2 = Vector2.zero;

		private float _prevDist = 0.0f;

		private Vector3 _startDragPos = Vector3.zero;
		[SerializeField]
		private Vector3 _targetDragPos = Vector3.zero;

		private Vector3 _lerpPos;

		private Plane _groundPlane;
		private void UpdateTouch1()
		{
			if( _levelStateControl.IsPlaying == false )
			{
				return;
			}
			CheckBorders();
			foreach( Touch _t in Input.touches )
			{
				if( _t.phase == TouchPhase.Began )
				{
					checkBegan(_t);
				}
				else if( _t.phase == TouchPhase.Ended )
				{
					checkEnded(_t);
				}
				else if( _t.phase == TouchPhase.Moved )
				{
					checkMoved(_t);
				}
				else if( _t.phase == TouchPhase.Stationary )
				{
					//checkMoved(_t);
				}
			}
			void checkBegan( Touch _t )
			{
				if( EventSystem.current.IsPointerOverGameObject(_t.fingerId) )
				{
					return;
				}
				if( _fingerIdMove1 == -1 )
				{
					_fingerIdMove1 = _t.fingerId;
					_fingerPos1 = _t.position;
					_startDragPos = -GetMousePointOnGround(new Vector3(_t.position.x , _t.position.y , 0.0f) , DragDisabledCamera) - _targetDragPos;
				}
				if( _fingerIdMove2 == -1 )
				{
					if( _fingerIdMove1 != _t.fingerId )
					{
						_fingerIdMove2 = _t.fingerId;
						_fingerPos2 = _t.position;
						_prevDist = Vector2.Distance(_fingerPos1 , _fingerPos2);
						Vector2 v = Vector2.Lerp(_fingerPos1 , _fingerPos2 , 0.5f);
						_startDragPos = -GetMousePointOnGround(new Vector3(v.x , v.y , 0.0f) , DragDisabledCamera) - _targetDragPos;
					}
				}
			}
			void checkEnded( Touch _t )
			{
				if( _fingerIdMove1 == _t.fingerId )
				{
					_fingerIdMove1 = -1;
				}
				if( _fingerIdMove2 == _t.fingerId )
				{
					_fingerIdMove2 = -1;
				}
			}
			void checkMoved( Touch _t )
			{
				if( _fingerIdMove1 == _t.fingerId )
				{
					_fingerPos1 = _t.position;
				}
				if( _fingerIdMove2 == _t.fingerId )
				{
					_fingerPos2 = _t.position;
				}
			}

			if( _fingerIdMove1 != -1 )
			{
				if( _fingerIdMove2 != -1 )
				{
					checkScale();
					Vector2 v = Vector2.Lerp(_fingerPos1 , _fingerPos2 , 0.5f);
					CheckDrag(v);
				}
				else
				{
					CheckDrag(_fingerPos1);
				}
			}

			void checkScale()
			{
				float currDist = Vector2.Distance(_fingerPos1 , _fingerPos2);
				float delata = currDist - _prevDist;
				_gamePlayCameras.ChangeScaleMainCamera(delata);
				_prevDist = currDist;
			}

			void CheckDrag( Vector2 pos )
			{
				_targetDragPos = -GetMousePointOnGround(new Vector3(pos.x , pos.y , 0.0f) , DragDisabledCamera) - _startDragPos;

				float val = _cameraBorders.LeftPos.x + Mathf.Abs(_leftViewObj.localPosition.x);
				if( _targetDragPos.x <= val )
				{
					_targetDragPos.x = val;
				}
				val = _cameraBorders.RightPos.x - Mathf.Abs(_rightViewObj.localPosition.x);
				if( _targetDragPos.x >= val )
				{
					_targetDragPos.x = val;
				}
				val = _cameraBorders.TopPos.z - Mathf.Abs(_topViewObj.localPosition.z);
				if( _targetDragPos.z >= val )
				{
					_targetDragPos.z = val;
				}
				val = _cameraBorders.BottomPos.z + Mathf.Abs(_bottomViewObj.localPosition.z);
				if( _targetDragPos.z <= val )
				{
					_targetDragPos.z = val;
				}

				_dragDisabledObj.transform.position = _targetDragPos;

				_lerpPos.y = CameraContainerTransform.position.y;
				_lerpPos.x = _targetDragPos.x;
				_lerpPos.z = _targetDragPos.z;
			}

			if( Vector3.Distance(_lerpPos , CameraContainerTransform.position) > 0.2f )
			{
				float interpolation = 10.0f * Time.deltaTime;
				Vector3 _currPos = CameraContainerTransform.position;
				_currPos.x = Mathf.Lerp(CameraContainerTransform.position.x , _lerpPos.x , interpolation);
				_currPos.z = Mathf.Lerp(CameraContainerTransform.position.z , _lerpPos.z , interpolation);
				CameraContainerTransform.position = _currPos;
			}
		}
		private void UpdateTouch()
		{
			if( _levelStateControl.IsPlaying == false )
			{
				return;
			}
			CheckBorders();
			if( _fingerIdMove1 == -1 )
			{
				foreach( Touch _t in Input.touches )
				{
					if( _t.phase != TouchPhase.Began )
					{
						break;
					}
					if( EventSystem.current.IsPointerOverGameObject(_t.fingerId) )
					{
						break;
					}
					_fingerIdMove1 = _t.fingerId;
					_startDragPos = -GetMousePointOnGround(new Vector3(_t.position.x , _t.position.y , 0.0f) , DragDisabledCamera) - _targetDragPos/*_dragDisabledObj.transform.position*/;
					break;
				}
			}
			else
			{
				foreach( Touch _t in Input.touches )
				{
					if( _t.phase == TouchPhase.Ended )
					{
						CheckEnded(_t);
					}
					else if( _t.phase == TouchPhase.Moved )
					{
						CheckMove(_t);
					}
					else if( _t.phase == TouchPhase.Stationary )
					{
						CheckStationary(_t);
					}
				}
			}

			void CheckEnded( Touch _t )
			{
				if( _fingerIdMove1 != _t.fingerId )
				{
					return;
				}
				_fingerIdMove1 = -1;
			}
			void CheckStationary( Touch _t )
			{
				if( _fingerIdMove1 != _t.fingerId )
				{
					return;
				}
				CheckDrag(_t);
			}
			void CheckMove( Touch _t )
			{
				if( _fingerIdMove1 != _t.fingerId )
				{
					return;
				}
				CheckDrag(_t);
			}

			void CheckDrag( Touch _t )
			{
				_targetDragPos = -GetMousePointOnGround(new Vector3(_t.position.x , _t.position.y , 0.0f) , DragDisabledCamera) - _startDragPos;

				float val = _cameraBorders.LeftPos.x + Mathf.Abs(_leftViewObj.localPosition.x);
				if( _targetDragPos.x <= val )
				{
					_targetDragPos.x = val;
				}
				val = _cameraBorders.RightPos.x - Mathf.Abs(_rightViewObj.localPosition.x);
				if( _targetDragPos.x >= val )
				{
					_targetDragPos.x = val;
				}
				val = _cameraBorders.TopPos.z - Mathf.Abs(_topViewObj.localPosition.z);
				if( _targetDragPos.z >= val )
				{
					_targetDragPos.z = val;
				}
				val = _cameraBorders.BottomPos.z + Mathf.Abs(_bottomViewObj.localPosition.z);
				if( _targetDragPos.z <= val )
				{
					_targetDragPos.z = val;
				}

				_dragDisabledObj.transform.position = _targetDragPos;

				_lerpPos.y = CameraContainerTransform.position.y;
				_lerpPos.x = _targetDragPos.x;
				_lerpPos.z = _targetDragPos.z;
			}

			if( Vector3.Distance(_lerpPos , CameraContainerTransform.position) > 0.2f )
			{
				float interpolation = 10.0f * Time.deltaTime;
				Vector3 _currPos = CameraContainerTransform.position;
				_currPos.x = Mathf.Lerp(CameraContainerTransform.position.x , _lerpPos.x , interpolation);
				_currPos.z = Mathf.Lerp(CameraContainerTransform.position.z , _lerpPos.z , interpolation);
				CameraContainerTransform.position = _currPos;
			}
		}



		private void CheckBorders()
		{
			_leftViewObj.position = GetMousePointOnGround(new Vector3(0.0f , Screen.height / 2 , 0.0f) , MainCamera);
			_rightViewObj.position = GetMousePointOnGround(new Vector3(Screen.width , Screen.height / 2 , 0.0f) , MainCamera);
			_topViewObj.position = GetMousePointOnGround(new Vector3(Screen.width / 2 , Screen.height , 0.0f) , MainCamera);
			_bottomViewObj.position = GetMousePointOnGround(new Vector3(Screen.width / 2 , 0.0f , 0.0f) , MainCamera);
		}


		private Vector3 GetMousePointOnGround( Vector3 pos , Camera cam )
		{
			// get a ray out of the camera starting at the mouse position
			Ray ray = cam.ScreenPointToRay(pos);
			//Debug.DrawRay(ray)

			// check if the ray intersects the plane (and at what distance along the ray)
			if( _groundPlane.Raycast(ray , out float distanceToGround) )
			{
				// get the point along the ray using the returned distance
				return ray.GetPoint(distanceToGround);
			}

			// if it fails, return the last clicked position
			// should never happen unless your camera tilts past the horizon
			return _startDragPos;
		}
	}
}
