using Mamont.Data.SaveData;
using Mamont.EventsBus;
using Mamont.EventsBus.Signals;
using Mamont.Log;

namespace Mamont.Data.DataControl.Resources
{
	public class GameResourceService:IGameResourceService
	{
		private readonly ILogService _logService;
		private readonly IEventBusService _eventBusService;
		private readonly ISavedDataService _savedDataService;
		public GameResourceService( ILogService _logService, IEventBusService _eventBusService, ISavedDataService _savedDataService )
		{
			this._logService = _logService;
			this._eventBusService = _eventBusService;
			this._savedDataService = _savedDataService;

			if( this._savedDataService.IsDataLoaded )
			{
				InitStorageObject();
			}
			else
			{
				this._eventBusService.Subscribe<SavedDataLoadedSignal>(OnSavedDataLoadedSignal);
			}
		}

		private void OnSavedDataLoadedSignal( SavedDataLoadedSignal signal )
		{
			_eventBusService.Unsubscribe<SavedDataLoadedSignal>(OnSavedDataLoadedSignal);
			InitStorageObject();
		}

		private ResourcesStorageObject _storageObject;
		private void InitStorageObject()
		{
			_storageObject = new ResourcesStorageObject(_savedDataService);
		}


		public float GetCount( GameResourceType? type )
		{
			if( type == null )
			{
				return 0.0f;
			}
			return _storageObject.GetResCount((GameResourceType)type);
		}
		public bool EnoughResource( GameResourceType? type , float count )
		{
			if( type == null )
			{
				return true;
			}
			return _storageObject.GetResCount((GameResourceType)type) >= count;
		}
		public void AddResource( GameResourceType? type , float count )
		{
			if( type == null )
			{
				return;
			}
			float newCount = _storageObject.GetResCount((GameResourceType)type) + count;
			_storageObject.SetResCount((GameResourceType)type , newCount);
			_savedDataService.SaveGame();
			_eventBusService.Invoke(new ResourceCountChangedSignal((GameResourceType)type , newCount));


		}
		public void DecResource( GameResourceType? type , float count )
		{
			if( type == null )
			{
				return;
			}
			float newCount = _storageObject.GetResCount((GameResourceType)type) - count;
			if( newCount < 0.0f )
			{
				newCount = 0.0f;
				_logService.LogError($"Resource < 0 {type}");
			}
			_storageObject.SetResCount((GameResourceType)type , newCount);
			_savedDataService.SaveGame();
			_eventBusService.Invoke(new ResourceCountChangedSignal((GameResourceType)type , newCount));

		}
	}
}
