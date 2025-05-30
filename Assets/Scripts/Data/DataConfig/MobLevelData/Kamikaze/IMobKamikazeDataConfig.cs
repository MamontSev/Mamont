namespace Mamont.Data.DataConfig.Mob
{
	public interface IMobKamikazeDataConfig
	{
		int MaxMobLevel
		{
			get;
		}
		MobKamikazeLevelData GetMobLevelData( int level );
	}
}


