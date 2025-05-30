using Mamont.Data.DataConfig.Mob;

using UnityEngine;

namespace Mamont.Data.DataControl.Mob
{
	public class MobMeleeDataControl:IMobMeleeDataControl
	{
		public MobMeleeDataControl(IMobMeleeLevelDataConfig mobLevelDataConfig )
		{
			this.mobLevelDataConfig = mobLevelDataConfig;
		}

		private readonly IMobMeleeLevelDataConfig mobLevelDataConfig;
		private MobMeleeFightLevelData GetMobLevelData( int level )
		{
			return mobLevelDataConfig.GetMobLevelData(level);
		}

		public int MaxMobLevel => mobLevelDataConfig.MaxMobLevel;
		

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

		public float WalkSpeed( int level )
		{
			return GetMobLevelData(level).WalkSpeed;
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
		public float AttackDistance( int level )
		{
			return GetMobLevelData(level).AttackDistance;
		}
		public float AggresionRadius( int level )
		{
			return GetMobLevelData(level).AggresionRadius;
		}
		public float MinAgressionRadius( int level ) => 0.0f;
	}
}
