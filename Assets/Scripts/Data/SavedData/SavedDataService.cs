using System;
using System.Collections.Generic;

using Mamont.Data.SaveData.SaveLoad;
using Mamont.Events;
using Mamont.Events.Signals;
using Mamont.Log;

using UnityEngine;

namespace Mamont.Data.SaveData
{
	public class SavedDataService:ISavedDataService
	{
		private readonly ISaveLoadService _saveLoadService;
		private ILogService _logService;
		private readonly IEventBusService _eventBusService;

		public SavedDataService( ISaveLoadService saveLoadServ, ILogService logService, IEventBusService eventBusService )
		{
			_saveKey = "save1.json";
			_savedData = new();
			_saveLoadService = saveLoadServ;
			_logService = logService;
			_eventBusService = eventBusService;
		}


		

		protected SavedData _savedData;

		
		private readonly string _saveKey;

		public bool IsDataLoaded
		{
			get;
			private set;
		} = false;

		public void LoadData( Action onComplete )
		{
			_saveLoadService.Load<SavedData>(_saveKey ,
			data =>
			{
				_savedData = data;
				_logService.Log("Data loaded");
				complete();
			} ,
			() =>
			{
				// First Start
				_logService.Log("First Start");
				complete();
			} ,
			s =>
			{
				// Fail Load data
				_logService.LogError(s);
				complete();
			});

			void complete()
			{
				_eventBusService.Invoke(new SavedDataLoadedSignal());
				IsDataLoaded = true;
				onComplete?.Invoke();
				
			}
		}

		public void SaveGame()
		{
			SaveBaseStorageObjList();
			_saveLoadService.Save(_saveKey , _savedData);
		}

		public int GameStartCounter
		{
			get => _savedData.GameStartCounter;
			set => _savedData.GameStartCounter = value;
		}
		public bool UseVibration
		{
			get => _savedData.UseVibration;
			set => _savedData.UseVibration = value;
		}

		

		private List<IStorageObject> _baseStorageObjList = new();
		public void InitBaseStorageObj( IStorageObject storageObject )
		{
			_baseStorageObjList.Add(storageObject);

			string key = storageObject.GetKey();
			if( _savedData.BaseStorage.ContainsKey(key) == false )
			{
				_savedData.BaseStorage[key] = storageObject.GetSavedData();
			}
			else
			{
				storageObject.SetSavedData(_savedData.BaseStorage[key]);
			}
		}
		private void SaveBaseStorageObjList()
		{
			foreach( var obj in _baseStorageObjList )
			{
				_savedData.BaseStorage[obj.GetKey()] = obj.GetSavedData();
			}
		}
	}
}
