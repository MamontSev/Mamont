using System;
using System.Collections.Generic;

using UnityEngine;

namespace Mamont.Gameplay.Control.Damage
{
	public class DamageService:IDamageService
	{


		private List<IDamagableByCharacter> _damagableByZombie = new();
		public List<IDamagableByCharacter> DamagableByZombieList => _damagableByZombie;


		private List<IDamagableByMob> _damagableByMobList = new();
		public List<IDamagableByMob> DamagableByMobList => _damagableByMobList;


		private List<IDamagable> damageList = new();
		public void MakeDamage<T>( T traget , float damage , int groupAttackCount , float groupAttackRadius , DamageType type ) where T : IDamagable
		{
			if( traget == null )
			{
				return;
			}
			if( traget.HealthControl.MayDamageMe() == false )
			{
				return;
			}
			traget.HealthControl.DamageMe(damage , type);

			if( groupAttackCount < 1 )
			{
				return;
			}

			damageList.Clear();
			Vector3 targetPos = traget.Position;

			if( typeof(T) == typeof(IDamagableByMob) )
			{
				foreach( var obj in DamagableByMobList )
				{
					checkObj(obj);
					if( damageList.Count == groupAttackCount )
					{
						break;
					}
				}
			}
			else if( typeof(T) == typeof(IDamagableByCharacter) )
			{
				foreach( var obj in DamagableByZombieList )
				{

					checkObj(obj);
					if( damageList.Count == groupAttackCount )
					{
						break;
					}
				}
			}

			for( int i = damageList.Count - 1; i >= 0; i-- )
			{
				if( damageList[i] == null )
				{
					Debug.Log("obj is null");
					continue;
				}
				if( damageList[i].HealthControl.MayDamageMe() == false )
				{
					continue;
				}
				damageList[i].HealthControl.DamageMe(damage , type);
			}

			void checkObj( IDamagable obj )
			{
				if( obj == null )
				{
					return;
				}
				if( obj.HealthControl.MayDamageMe() == false )
				{
					return;
				}
				if( obj == (IDamagable)traget )
				{
					return;
				}
				if( Math.Abs(targetPos.x - obj.Position.x) > groupAttackRadius )
				{
					return;
				}
				if( Math.Abs(targetPos.z - obj.Position.z) > groupAttackRadius )
				{
					return;
				}
				float dist = Vector3.Distance(targetPos , obj.Position);
				if( dist > groupAttackRadius )
				{
					return;
				}

				damageList.Add(obj);
			}
		}


		private List<IDamagableByCharacter> _toxicCloudTargetList = new();
		private const float _toxicDamageDist = 2.5f;
		public void DamageByToxicCloud( Vector3 pos , float damage )
		{
			_toxicCloudTargetList.Clear();
			foreach( var obj in DamagableByZombieList )
			{
				if( obj == null )
				{
					continue;
				}
				if( obj.HealthControl.MayDamageMe() == false )
				{
					continue;
				}

				if( Math.Abs(pos.x - obj.Position.x) > _toxicDamageDist )
				{
					continue;
				}
				if( Math.Abs(pos.z - obj.Position.z) > _toxicDamageDist )
				{
					continue;
				}
				_toxicCloudTargetList.Add(obj);
			}
			if( _toxicCloudTargetList.Count == 0 )
			{
				return;
			}

			for( int i = _toxicCloudTargetList.Count - 1; i >= 0; i-- )
			{
				if( _toxicCloudTargetList[i] == null )
				{
					Debug.Log("obj is null");
					continue;
				}
				if( _toxicCloudTargetList[i].HealthControl.MayDamageMe() == false )
				{
					continue;
				}
				_toxicCloudTargetList[i].HealthControl.DamageMe(damage , DamageType.ToxicCloud);
			}
		}

		private List<IDamagable> _damageBombList = new();
		public void DamageByBomb<T>( Vector3 boomPos , float damage , float radius , float damageCenter , float damageEdge ) where T : IDamagable
		{
			if( radius <= 0.0f )
			{
				radius = 3.0f;
			}
			_damageBombList.Clear();

			if( typeof(T) == typeof(IDamagableByMob) )
			{
				foreach( var obj in DamagableByMobList )
				{
					checkObj(obj);
				}
			}
			else if( typeof(T) == typeof(IDamagableByCharacter) )
			{
				foreach( var obj in DamagableByZombieList )
				{
					checkObj(obj);
				}
			}
			void checkObj( IDamagable obj )
			{
				if( obj == null )
				{
					return;
				}
				if( obj.HealthControl.MayDamageMe() == false )
				{
					return;
				}
				if( Math.Abs(boomPos.x - obj.Position.x) > radius )
				{
					return;
				}
				if( Math.Abs(boomPos.z - obj.Position.z) > radius )
				{
					return;
				}
				float dist = Vector3.Distance(boomPos , obj.Position);
				if( dist > radius )
				{
					return;
				}
				_damageBombList.Add(obj);
			}

			for( int i = _damageBombList.Count - 1; i >= 0; i-- )
			{
				if( _damageBombList[i] == null )
				{
					continue;
				}
				if( _damageBombList[i].HealthControl.MayDamageMe() == false )
				{
					continue;
				}

				float distToCenter = Vector3.Distance(_damageBombList[i].Position , boomPos);
				float distK = 1 - ( distToCenter / radius );
				float damageDelta = damageCenter - damageEdge;
				float newDamage = ( damageEdge + damageDelta * distK ) * damage;

				//Debug.Log($"{_damageBombList[i].Name}; distToCenter {distToCenter}; distK {distK};  damageDelta {damageDelta}; damage {damage}; newDamage {newDamage} ");

				_damageBombList[i].HealthControl.DamageMe(newDamage , DamageType.Bomb);
			}
		}


	}
}
