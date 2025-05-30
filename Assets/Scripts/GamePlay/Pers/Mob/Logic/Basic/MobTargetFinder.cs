using System;

using Mamont.Gameplay.Control.Damage;
using Mamont.Gameplay.Level.Colliers;

using UnityEngine;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MobTargetFinder
	{
		private readonly MobBasic _mobBasic;
		private readonly IDamageService _damageService;
		public MobTargetFinder
		(
			MobBasic _mobBasic ,
			IDamageService _damageService
		)
		{
			this._mobBasic = _mobBasic;
			this._damageService = _damageService;
		}
		public IDamagableByMob CurrTarget
		{
			get;
			private set;
		} = null;

		public bool IsCorrectTarget()
		{
			if( CurrTarget == null )
				return false;
			if( CurrTarget.HealthControl.MayDamageMe() == false )
				return false;
			return true;
		}


		public IDamagableByMob FindTarget()
		{
			(IDamagableByMob obj, float dist) newTraget = (null, float.MaxValue);
			Vector3 selfPos = _mobBasic.InContainer.position;

			float radius = _mobBasic.AgressionRadius;
			float minRadius = _mobBasic.MinAgressionRadius;


			foreach( var obj in _damageService.DamagableByMobList )
			{
				if( obj == null )
				{
					continue;
				}
				if( obj.HealthControl.MayDamageMe() == false )
				{
					continue;
				}


				Vector3 objPos = obj.Position;
				float deltaX = Math.Abs(selfPos.x - obj.Position.x);
				if( deltaX > radius )
				{
					continue;
				}
				float deltaZ = Math.Abs(selfPos.z - obj.Position.z);
				if( deltaZ > radius )
				{
					continue;
				}
				float dist = Vector3.Distance(selfPos , obj.Position);
				if( dist > radius )
				{
					continue;
				}
				if( dist < minRadius )
				{
					continue;
				}
				Vector3 v1 = new Vector3(selfPos.x , selfPos.y + 1.0f , selfPos.z);
				Vector3 v2 = new Vector3(objPos.x , objPos.y + 1.0f , objPos.z);
				Debug.DrawRay(v1 , v2 - v1 , Color.green , 0.2f , false);
				RaycastHit[] hits = Physics.RaycastAll(v1 , v2 - v1 , dist);

				bool findObstacle = false;
				for( int i = 0; i < hits.Length; i++ )
				{
					RaycastHit hit = hits[i];
					if( hit.transform == null )
					{
						continue;
					}
					bool b = hit.transform.TryGetComponent(out IMobObstacleCollider obstacle);
					if( b == true )
					{
						findObstacle = true;
						break;
					}
				}
				if( findObstacle == true )
				{
					continue;
				}

				if( dist < newTraget.dist )
				{
					newTraget = (obj, dist);
				}
			}

			CurrTarget = newTraget.obj;
			return CurrTarget;
		}
	}
}
