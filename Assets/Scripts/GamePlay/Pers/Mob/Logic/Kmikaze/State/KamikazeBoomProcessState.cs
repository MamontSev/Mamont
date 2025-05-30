using Mamont.Data.DataControl.Mob;
using Mamont.Gameplay.Control.Damage;

using Manmont.Tools.StateMashine;

namespace Mamont.Gameplay.Pers.Mob
{
	public class KamikazeBoomProcessState:IMobState, IEnterState
	{
		private readonly MobBasic _mobBasic;
		private readonly MobStateMachine _stateMachine;
		private readonly IMobKamikazeDataControl _selfDataControl;
		private readonly IDamageService _damageService;
		public KamikazeBoomProcessState
		(
			MobBasic _mobBasic ,
			MobStateMachine _stateMachine ,
			IMobKamikazeDataControl _selfDataControl,
			IDamageService _damageService
		)
		{
			this._mobBasic = _mobBasic;
			this._stateMachine = _stateMachine;
			this._selfDataControl = _selfDataControl;
			this._damageService = _damageService;
		}

		private MobSkinKamikaze SelfSkin => _mobBasic.Skin as MobSkinKamikaze;
		private MobKamikaze Self => _mobBasic as MobKamikaze;

		public void Enter()
		{
			_mobBasic.NavMeshAgent.enabled = true;
			_mobBasic.NavMeshAgent.speed = 0.0f;

			SelfSkin.PlayBoomPrepare(MakeBoom);

			void MakeBoom()
			{
				if( _stateMachine.CurrState is not KamikazeBoomProcessState )
				{
					return;
				}
				Self.CreateBoomEffect(SelfSkin.BoomEffect.gameObject,_mobBasic.InContainer.position);

				_damageService.DamageByBomb<IDamagableByMob>
				(
				_mobBasic.InContainer.position ,
				_selfDataControl.Damage(_mobBasic.SelfLevel) ,
				_selfDataControl.AttackRadius(_mobBasic.SelfLevel) ,
				_selfDataControl.AttackCenter(_mobBasic.SelfLevel) ,
				_selfDataControl.AttackEdge(_mobBasic.SelfLevel)
				);

				_stateMachine.Enter<KamikazeBoomedState>();
			}
		}


	}
}

