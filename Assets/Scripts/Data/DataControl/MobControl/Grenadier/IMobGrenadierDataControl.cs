namespace Mamont.Data.DataControl.Mob
{
	public interface IMobGrenadierDataControl:IMobBasicDataControl
	{
		float AttackRadius( int level );
		float AttackCenter( int level );
		float AttackEdge( int level );
		float BeatInterval( int level );
	}
}

