using UnityEngine;

using Zenject;

namespace Mamont.Gameplay.Control.Damage
{
	[RequireComponent(typeof(Rigidbody))]	  
	public class ThrowBomb:MonoBehaviour
	{
		private IDamageService _damageControl;

		[Inject]
		private void Construct( IDamageService _damageControl )
		{
			this._damageControl = _damageControl;
		}

		[Header("Эффект взрыва - тип BoomEffect")]
		[SerializeField]
		private BoomEffect _boomEffect;

		private float _damage;
		private float _radius;
		private float _damageCenter;
		private float _damageEdge;
		public void Init( float damage,float radius, float damageCenter, float damageEdge, Vector3 linearVelocity ) 
		{
			_damage = damage;
			_radius = radius;										   
			_damageCenter = damageCenter;
			_damageEdge = damageEdge;
			GetComponent<Rigidbody>().linearVelocity = linearVelocity;
		}

		private void OnCollisionEnter( Collision collision )
		{
			Vector3 damagePos = new Vector3(transform.position.x,0.0f,transform.position.z);
			_damageControl.DamageByBomb<IDamagableByMob>(damagePos , _damage , _radius , _damageCenter , _damageEdge);

			GameObject effect =  GameObject.Instantiate(_boomEffect.gameObject);
			effect.transform.position = transform.position;

			Destroy(gameObject);
		}

	}
}
