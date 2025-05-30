namespace Mamont.Data.DataControl.Mob
{
	public interface IMobKamikazeDataControl:IMobBasicDataControl
	{
		float WalkSpeed( int level );
		float AttackRadius( int level );
		float AttackCenter( int level );
		float AttackEdge( int level );
		float AttackDistance( int level );
	}
}


