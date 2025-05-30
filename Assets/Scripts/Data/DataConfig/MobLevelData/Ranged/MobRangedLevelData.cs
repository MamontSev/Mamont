namespace Mamont.Data.DataConfig.Mob
{
	public class MobRangedLevelData
	{
		public readonly float Health;
		public readonly float DamageMin; 
		public readonly float DamageMax; 
		public readonly float GroupAttack;
		public readonly float GroupAttackRadius;
		public readonly float CritDamage;
		public readonly float CritChanse;
		public readonly float BeatInterval;
		public readonly float AggresionRadius;

		public MobRangedLevelData(
		float Health ,
		float DamageMin ,
		float DamageMax ,
		float GroupAttack ,
		float GroupAttackRadius ,
		float CritDamage ,
		float CritChanse ,
		float BeatInterval ,
		float AggresionRadius 
		)
		{
			this.Health = Health;
			this.DamageMin = DamageMin;
			this.DamageMax = DamageMax;
			this.GroupAttack = GroupAttack;
			this.GroupAttackRadius = GroupAttackRadius;
			this.CritDamage = CritDamage;
			this.CritChanse = CritChanse;
			this.BeatInterval = BeatInterval;
			this.AggresionRadius = AggresionRadius;
		}
	}
}
