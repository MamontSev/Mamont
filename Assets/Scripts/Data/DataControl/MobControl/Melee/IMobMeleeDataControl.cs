namespace Mamont.Data.DataControl.Mob
{
	public interface IMobMeleeDataControl:IMobBasicDataControl
	{
		float WalkSpeed( int level );	
		int GroupAttack( int level );
		float GroupAttackRadius( int level );
		float BeatInterval( int level );
		float AttackDistance( int level ); 

	}
}
