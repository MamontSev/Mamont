using UnityEngine;

namespace Mamont.Gameplay.Pers
{
	public class MovableObjSpeedControl
	{
		private Transform _selfTransform = null;
		public void Init( Transform _selfTransform )
		{
			this._selfTransform = _selfTransform;
			_prevPos = this._selfTransform.position;
		}
		private Vector3 _prevPos;
		public void Update()
		{
			if( _selfTransform == null )
			{
				return;
			}
			if( Time.deltaTime == 0.0f )
			{
				CurrSpeed = Vector3.zero;
				return;
			}
			CurrSpeed = ( _selfTransform.position - _prevPos ) / Time.deltaTime;
			_prevPos = _selfTransform.position;
		}

		public Vector3 CurrSpeed
		{
			get;
			private set;
		} = Vector3.zero;
	}
}
