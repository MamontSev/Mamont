using Mamont.Data.DataConfig.Mob;

using UnityEngine;

namespace Mamont.Data.DataControl.Mob
{
	public class MobRangedDataControl :IMobRangedDataControl
	{
		public MobRangedDataControl( IMobRangedLevelDataConfig mobFarFightLevelDataConfig )
		{
			this.mobFarFightLevelDataConfig = mobFarFightLevelDataConfig;
		}

		private readonly IMobRangedLevelDataConfig mobFarFightLevelDataConfig;
		private MobRangedLevelData GetMobLevelData( int level )
		{
			return mobFarFightLevelDataConfig.GetMobLevelData(level);
		}

		public int MaxMobLevel => mobFarFightLevelDataConfig.MaxMobLevel;


		public float StartHealth( int level )
		{
			return GetMobLevelData(level).Health;
		}

		public float Damage( int level )
		{
			float damage = UnityEngine.Random.Range(GetMobLevelData(level).DamageMin , GetMobLevelData(level).DamageMax);

			float CritChanse = GetMobLevelData(level).CritChanse;
			if( UnityEngine.Random.value < CritChanse )
			{
				damage += GetMobLevelData(level).CritDamage;
			}
			return damage;
		}

		public int GroupAttack( int level )
		{
			return (int)Mathf.Ceil(GetMobLevelData(level).GroupAttack);
		}
		public float GroupAttackRadius( int level )
		{
			return GetMobLevelData(level).GroupAttackRadius;
		}
		public float BeatInterval( int level )
		{
			return GetMobLevelData(level).BeatInterval;
		}
		public float AggresionRadius( int level )
		{
			return GetMobLevelData(level).AggresionRadius;
		}
		public float MinAgressionRadius( int level ) => 0.0f;
	}
}
