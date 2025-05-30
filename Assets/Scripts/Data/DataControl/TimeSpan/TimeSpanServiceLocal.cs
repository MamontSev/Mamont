using System;

using Mamont.Data.DataConfig;
using Mamont.Data.SaveData;
using Mamont.EventsBus;
using Mamont.EventsBus.Signals;

namespace Mamont.Data.DataControl.TimeSpanControl
{
	public class TimeSpanServiceLocal:ITimeSpanService
	{
		private readonly ISavedDataService _savedDataService;
		private readonly ITimeSpanDataConfig _timeSpanDataConfig;
		private readonly IEventBusService _eventBusService;
		public TimeSpanServiceLocal( ISavedDataService _savedDataService , ITimeSpanDataConfig _timeSpanDataConfig, IEventBusService _eventBusService )
		{
			this._savedDataService = _savedDataService;
			this._timeSpanDataConfig = _timeSpanDataConfig;
			this._eventBusService = _eventBusService;

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

		private ITimeSpanStorageObject _storageObj;
		private void InitStorageObject()
		{
			_storageObj = new TimeSpanStorageObject(_savedDataService);
		}

		public void Start( TimeSpanType type )
		{
			_storageObj.Start(type,DateTime.UtcNow.ToFileTimeUtc());
			_savedDataService.SaveGame();
		}

		public void Stop( TimeSpanType type )
		{
			_storageObj.Stop(type);
			_savedDataService.SaveGame();
		}

		public void CheckState( TimeSpanType type , Action onNotStarted = null , Action<long> onContinue = null , Action onComplete = null )
		{
			if( _storageObj.IsWorking(type) == false )
			{
				onNotStarted?.Invoke();
				return;
			}
			DateTime nowUTC = DateTime.UtcNow;
			DateTime startUTC = DateTime.FromFileTimeUtc(_storageObj.GetStartTime(type));
			TimeSpan timeSpan = nowUTC - startUTC;
			long secondPassed = Convert.ToInt64(timeSpan.TotalSeconds);
			long secondsRemaining = _timeSpanDataConfig.GetAwaitSeconds(type) - secondPassed;

			if( secondsRemaining > 0 )
			{
				onContinue?.Invoke(secondsRemaining);
				return;
			}
			onComplete?.Invoke();
		}
	}
}
