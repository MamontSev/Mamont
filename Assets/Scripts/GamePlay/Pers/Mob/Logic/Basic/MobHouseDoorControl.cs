using Mamont.EventsBus;
using Mamont.EventsBus.Signals;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MobHouseDoorControl
	{
		private readonly MobBasic _mobBasic;
		private readonly IEventBusService _busService;
		public MobHouseDoorControl
		(
			MobBasic _mobBasic ,
			IEventBusService _busService
		)
		{
			this._mobBasic = _mobBasic;
			this._busService = _busService;

			_mobBasic.OnDisableAction += OnDisabele;
		}

		public bool IsInHouse
		{
			get;
			private set;
		} = false;

		private string _doorName;
		public void Init( string _doorName )
		{
			this._doorName = _doorName;
			if( _doorName == "" )
			{
				IsInHouse = false;
				return;
			}
			IsInHouse = true;
			Subscribe();
		}


		private bool _isSubscribe = false;
		private void Subscribe()
		{
			if( _isSubscribe == true )
				return;
			_isSubscribe = true;
			_busService.Subscribe<HouseDoorBrokenSignal>(OnHouseBroken);
		}
		private void Unsubscribe()
		{
			if( _isSubscribe == false )
				return;
			_isSubscribe = false;
			_busService.Unsubscribe<HouseDoorBrokenSignal>(OnHouseBroken);
		}

		private void OnHouseBroken( HouseDoorBrokenSignal signal )
		{
			if( IsInHouse == false )
				return;
			if( signal.HouseName != _doorName )
				return;
			IsInHouse = false;
			Unsubscribe();
		}

		private void OnDisabele()
		{
			_mobBasic.OnDisableAction -= OnDisabele;
			Unsubscribe();
		}
	}
}
