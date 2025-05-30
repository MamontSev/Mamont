namespace Mamont.Data.DataConfig.Mob
{
	public interface IMobRangedLevelDataConfig
	{
		int MaxMobLevel
		{
			get;
		}
		MobRangedLevelData GetMobLevelData( int level );
	}
}
