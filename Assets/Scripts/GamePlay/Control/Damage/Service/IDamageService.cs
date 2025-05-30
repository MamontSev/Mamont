using System.Collections.Generic;

using UnityEngine;

namespace Mamont.Gameplay.Control.Damage
{
	public interface IDamageService
	{
		List<IDamagableByCharacter> DamagableByZombieList
		{
			get;
		}
		List<IDamagableByMob> DamagableByMobList
		{
			get;
		}

		void MakeDamage<T>( T traget , float damage , int groupAttackCount , float groupAttackRadius ,  DamageType type ) where T : IDamagable;
		void DamageByToxicCloud( Vector3 pos , float damage );
		void DamageByBomb<T>( Vector3 pos , float damage , float radius , float damageCenter , float damageEdge ) where T : IDamagable;
	}
}
