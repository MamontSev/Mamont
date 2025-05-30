using Mamont.Data.DataConfig.Mob;

namespace Mamont.Data.DataControl.Mob
{
	public class MobKamikazeDataControl:IMobKamikazeDataControl
	{
		public MobKamikazeDataControl( IMobKamikazeDataConfig mobKamikazeDataConfig )
		{
			this.mobKamikazeDataConfig = mobKamikazeDataConfig;
		}

		private readonly IMobKamikazeDataConfig mobKamikazeDataConfig;
		private MobKamikazeLevelData GetMobLevelData( int level )
		{
			return mobKamikazeDataConfig.GetMobLevelData(level);
		}

		public int MaxMobLevel => mobKamikazeDataConfig.MaxMobLevel;


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

		public float AttackRadius( int level )
		{
			return GetMobLevelData(level).AttackRadius;
		}
		public float AttackCenter( int level )
		{
			return GetMobLevelData(level).AttackCenter;
		}
		public float AttackEdge( int level )
		{
			return GetMobLevelData(level).AttackEdge;
		}
		public float AggresionRadius( int level )
		{
			return GetMobLevelData(level).AggresionRadius;
		}

		public float WalkSpeed( int level )
		{
			return GetMobLevelData(level).WalkSpeed;
		}

		public float AttackDistance( int level )
		{
			return GetMobLevelData(level).AttackDistance;
		}

		public float MinAgressionRadius( int level ) => 0.0f;
	}
}
