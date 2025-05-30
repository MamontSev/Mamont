namespace Mamont.Data.DataConfig.Mob
{
	public class MobMeleeFightLevelData
	{
		public readonly float Health;
		public readonly float DamageMin;
		public readonly float DamageMax; 
		public readonly float WalkSpeed;
		public readonly float GroupAttack; 
		public readonly float GroupAttackRadius; 
		public readonly float CritDamage; 
		public readonly float CritChanse; 
		public readonly float BeatInterval;	
		public readonly float AttackDistance;
		public readonly float AggresionRadius;




		public MobMeleeFightLevelData(
		float Health ,
		float DamageMin ,
		float DamageMax ,
		float WalkSpeed ,
		float GroupAttack ,
		float GroupAttackRadius ,
		float CritDamage ,
		float CritChanse ,
		float BeatInterval ,
		float AttackDistance ,
		float AggresionRadius
		)
		{
			this.Health = Health;
			this.DamageMin = DamageMin;
			this.DamageMax = DamageMax;
			this.WalkSpeed = WalkSpeed;
			this.GroupAttack = GroupAttack;
			this.GroupAttackRadius = GroupAttackRadius;
			this.CritDamage = CritDamage;
			this.CritChanse = CritChanse;
			this.BeatInterval = BeatInterval;
			this.AttackDistance = AttackDistance;
			this.AggresionRadius = AggresionRadius;
		}
	}
}
