using Mamont.Data.DataConfig.Mob;

namespace Mamont.Data.DataControl.Mob
{
	public class MobGrenadierDataControl:IMobGrenadierDataControl
	{
		public MobGrenadierDataControl(IMobGrenadierDataConfig mobGrenadierDataConfig )
		{
			this.mobGrenadierDataConfig = mobGrenadierDataConfig;
		}

		private readonly IMobGrenadierDataConfig mobGrenadierDataConfig;
		private MobGrenadierLevelData GetMobLevelData( int level )
		{
			return mobGrenadierDataConfig.GetMobLevelData(level);
		}

		public int MaxMobLevel => mobGrenadierDataConfig.MaxMobLevel;


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
		public float BeatInterval( int level )
		{
			return GetMobLevelData(level).BeatInterval;
		}
		public float AggresionRadius( int level )
		{
			return GetMobLevelData(level).AggresionRadius;
		}
		public float MinAgressionRadius( int level )
		{
			return GetMobLevelData(level).MinAgressionRadius;
		}
	}
}
