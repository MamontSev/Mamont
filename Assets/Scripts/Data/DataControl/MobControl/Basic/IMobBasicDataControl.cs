namespace Mamont.Data.DataControl.Mob
{
	public interface IMobBasicDataControl
	{
		int MaxMobLevel
		{
			get;
		}
		float StartHealth( int level );
		float Damage( int level );
		float AggresionRadius( int level );
		float MinAgressionRadius( int level );
	}
}
