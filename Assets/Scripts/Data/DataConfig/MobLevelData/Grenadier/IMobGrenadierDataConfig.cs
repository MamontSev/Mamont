namespace Mamont.Data.DataConfig.Mob
{
	public interface IMobGrenadierDataConfig
	{
		int MaxMobLevel
		{
			get;
		}
		MobGrenadierLevelData GetMobLevelData( int level );
	}
}

