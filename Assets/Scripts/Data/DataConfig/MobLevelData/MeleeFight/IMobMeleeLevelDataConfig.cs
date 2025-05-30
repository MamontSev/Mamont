namespace Mamont.Data.DataConfig.Mob
{
	public interface IMobMeleeLevelDataConfig
	{
		int MaxMobLevel
		{
			get;
		}
		MobMeleeFightLevelData GetMobLevelData( int level );
	}
}
