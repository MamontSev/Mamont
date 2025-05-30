using System;

using UnityEngine;

namespace Mamont.Gameplay.Control.Damage
{
	public class TestBomb:MonoBehaviour
	{

		[SerializeField] private GameObject _startGO;
		[SerializeField] private GameObject _endGO;

		[SerializeField] private GameObject _bulletPrefab;

		[SerializeField] private GameObject _spawnerGo;
		[SerializeField] private float _angle = 45.0f;

		private void Update()
		{
			_spawnerGo.transform.localEulerAngles = new Vector3(-_angle,0,0);
			Vector3 fromTo = _endGO.transform.position - _startGO.transform.position;

			Vector3 fromToXZ = new Vector3(fromTo.x , 0f , fromTo.z);

			_startGO.transform.rotation = Quaternion.LookRotation(fromToXZ , Vector3.up);
			if( Input.GetMouseButtonDown(0) )
			{
				Shot();
			}
		}

		public void Shot()
		{
			
			float angleRad = _angle * (float)Math.PI / 180.0f;



			Vector3 fromTo = _endGO.transform.position - _startGO.transform.position;

			Vector3 fromToXZ = new Vector3(fromTo.x , 0f , fromTo.z);

			_startGO.transform.rotation = Quaternion.LookRotation(fromToXZ , Vector3.up);

			float x = fromToXZ.magnitude;
			float y = fromTo.y;

			double v2 = ( Physics.gravity.y * x * x ) / ( 2 * ( y - Math.Tan(angleRad) * x ) * Math.Pow(Math.Cos(angleRad) , 2) );
			float v = (float)Math.Sqrt(Math.Abs(v2));

			GameObject bullet = Instantiate(_bulletPrefab , _startGO.transform.position , Quaternion.identity);
			bullet.GetComponent<Rigidbody>().linearVelocity = _spawnerGo.transform.forward * v;
		}
	}
}
