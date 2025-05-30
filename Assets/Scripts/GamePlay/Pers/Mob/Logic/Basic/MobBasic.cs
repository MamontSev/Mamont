using System;
using System.Collections.Generic;

using Mamont.Data.DataControl.Mob;
using Mamont.Gameplay.Control.Damage;
using Mamont.Gameplay.Control.LevelState;

using UnityEngine;
using UnityEngine.AI;

using Zenject;

namespace Mamont.Gameplay.Pers.Mob
{
	public abstract class MobBasic:MonoBehaviour, IDamagableByCharacter	, IMovableTarget
	{
		private ILevelStateControl _levelStateControl;
		private DamagableObjHealthControl _healthControl;
		private MovableObjSpeedControl _movableObjSpeedControl;
		private MobHouseDoorControl _doorControl;
		protected MobStateMachine _stateMachine;
		protected MobStateFactory _stateFactory;

		[Inject]
		private void Construct
		(
			ILevelStateControl _levelStateControl ,
			DamagableObjHealthControl _healthControl ,
			MovableObjSpeedControl _movableObjSpeedControl,
			MobHouseDoorControl _doorControl,
			MobStateMachine _stateMachine,
			MobStateFactory _stateFactory
		)
		{
			this._levelStateControl = _levelStateControl;
			this._healthControl = _healthControl;
			this._movableObjSpeedControl = _movableObjSpeedControl;
			this._doorControl = _doorControl;
			this._stateMachine = _stateMachine;
			this._stateFactory = _stateFactory;
		}

		private void OnEnable()
		{
			_healthControl.OnTakeDamage += OnHealthChanged;
		}
		public event Action OnDisableAction;
		private void OnDisable()
		{
			_healthControl.OnTakeDamage -= OnHealthChanged;
			OnDisableAction?.Invoke();
		}

		private void Update()
		{
			if( _levelStateControl.IsPlaying == false )
			{
				return;
			}
			_skin.UpdateMe(CurrSpeed.magnitude);
			_healthControl.Update();
			_movableObjSpeedControl.Update();

			_stateMachine.Update();
		}


		[SerializeField]
		protected NavMeshAgent _navMeshAgent;
		public NavMeshAgent NavMeshAgent => _navMeshAgent;

		[SerializeField]
		private Transform _skinContainer;

		[SerializeField]
		protected Transform _inContainer;
		public Transform InContainer => _inContainer;


		protected MobSkinBasic _skin;
		public MobSkinBasic Skin => _skin;

		public Quaternion Rotation => _inContainer.rotation;


		private int _selfLevel;
		public int SelfLevel => _selfLevel;

		public bool IsInHouse => _doorControl.IsInHouse;


		#region General Abilities

		protected IMobBasicDataControl _basicDataControl;
		public float StartHealth => _basicDataControl.StartHealth(_selfLevel);
		public float Damage => _basicDataControl.Damage(_selfLevel);
		public float AgressionRadius => _basicDataControl.AggresionRadius(_selfLevel);
		public float MinAgressionRadius => _basicDataControl.MinAgressionRadius(_selfLevel);

		#endregion

		#region  IDamagable	,IMovableTarget

		public Vector3 Position => _inContainer.position;
		public IDamagableObjHealthControl HealthControl => _healthControl;

		public Vector3 CurrSpeed => _movableObjSpeedControl.CurrSpeed;

		public string Name => name;

		#endregion


		#region Init
		public void Init
		(
			GameObject _skinPrefab ,
			int _selfLevel ,
			Vector3 _startPos ,
			string _houseDoorName = ""
		)
		{
			CreateSkin(_skinPrefab);
			this._selfLevel = _selfLevel;
			_navMeshAgent.radius = _skin.NavigationRadius;
			SetStartPosRot(_startPos);


			_doorControl.Init(_houseDoorName);
			

			_movableObjSpeedControl.Init(_inContainer);
			InitHealthComponent();
		}
		private void CreateSkin(GameObject _skinPrefab )
		{
			_skin = GameObject.Instantiate(_skinPrefab , _skinContainer).GetComponent<MobSkinBasic>();
		}
		private void SetStartPosRot( Vector3 _startPos )
		{
			float rand = UnityEngine.Random.Range(0.0f , 360.0f);
			Quaternion _startRot = Quaternion.Euler(0.0f , rand , 0.0f);
			_inContainer.SetPositionAndRotation(_startPos, _startRot);
		}
		private void InitHealthComponent()
		{
			_healthControl.Init(
					() => false ,
					MayDamageMe ,
					StartHealth ,
					_skin.HealthIndicatorParent);
		}
		#endregion



		private void OnHealthChanged( float health , DamageType damageType )
		{
			if( MayDamageMe() == false )
				return;
			if( health <= 0.0f )
				_stateMachine.Enter<MobKilledState>();
		}


		private readonly  List<Type> _canNotDamageTypeList = new()
		{
			typeof(MobStartAwaitState), typeof(MobKilledState)
		};
		protected virtual bool MayDamageMe()
		{
			if( IsInHouse == true )
				return false;

			Type t = _stateMachine.CurrState.GetType();

			return !_canNotDamageTypeList.Contains(t);
		}

	}
}
