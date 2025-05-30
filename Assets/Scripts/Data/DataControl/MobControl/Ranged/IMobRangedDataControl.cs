namespace Mamont.Data.DataControl.Mob
{
	public interface IMobRangedDataControl:IMobBasicDataControl
	{
		int GroupAttack( int level );
		float GroupAttackRadius( int level );
		float BeatInterval( int level );
	}
}
