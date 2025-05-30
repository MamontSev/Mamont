namespace Mamont.Data.DataConfig.Mob
{
	public class MobGrenadierLevelData
	{
		public readonly float Health;
		public readonly float DamageMin;
		public readonly float DamageMax;
		public readonly float AttackRadius;
		public readonly float AttackCenter;
		public readonly float AttackEdge;
		public readonly float CritDamage;
		public readonly float CritChanse;
		public readonly float BeatInterval;
		public readonly float AggresionRadius;
		public readonly float MinAgressionRadius;




		public MobGrenadierLevelData(
		float Health ,
		float DamageMin ,
		float DamageMax ,
		float AttackRadius ,
		float AttackCenter ,
		float AttackEdge ,
		float CritDamage ,
		float CritChanse ,
		float BeatInterval ,
		float AggresionRadius ,
		float MinAgressionRadius 
		)
		{
			this.Health = Health;
			this.DamageMin = DamageMin;
			this.DamageMax = DamageMax;
			this.AttackRadius = AttackRadius;
			this.AttackCenter = AttackCenter;
			this.AttackEdge = AttackEdge;
			this.CritDamage = CritDamage;
			this.CritChanse = CritChanse;
			this.BeatInterval = BeatInterval;
			this.AggresionRadius = AggresionRadius;
			this.MinAgressionRadius = MinAgressionRadius;
		}
	}
}

