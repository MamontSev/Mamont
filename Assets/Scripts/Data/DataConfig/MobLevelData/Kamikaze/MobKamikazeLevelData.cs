namespace Mamont.Data.DataConfig.Mob
{
	public class MobKamikazeLevelData
	{
		public readonly float Health;
		public readonly float DamageMin;
		public readonly float DamageMax;
		public readonly float WalkSpeed;
		public readonly float AttackRadius;
		public readonly float AttackCenter;
		public readonly float AttackEdge;
		public readonly float CritDamage;
		public readonly float CritChanse;
		public readonly float AttackDistance;
		public readonly float AggresionRadius;


		public MobKamikazeLevelData(
		float Health ,
		float DamageMin ,
		float DamageMax ,
		float WalkSpeed ,
		float AttackRadius ,
		float AttackCenter ,
		float AttackEdge ,
		float CritDamage ,
		float CritChanse ,
		float AttackDistance ,
		float AggresionRadius 
		)
		{
			this.Health = Health;
			this.DamageMin = DamageMin;
			this.DamageMax = DamageMax;
			this.WalkSpeed = WalkSpeed;
			this.AttackRadius = AttackRadius;
			this.AttackCenter = AttackCenter;
			this.AttackEdge = AttackEdge;
			this.CritDamage = CritDamage;
			this.CritChanse = CritChanse;
			this.AttackDistance = AttackDistance;
			this.AggresionRadius = AggresionRadius;
		}
	}
}




