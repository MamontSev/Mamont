using Mamont.Data.DataControl.Language.Storage;
using Mamont.Data.SaveData;
using Mamont.Events;
using Mamont.Events.Signals;

using UnityEngine;

namespace Mamont.Data.DataControl.Language
{
	public class LanguageControlService :ILanguageControlService
	{
		private readonly ISavedDataService _savedDataService;
		private readonly IEventBusService _eventBusService;

		public LanguageControlService( ISavedDataService savedDataService, IEventBusService eventBusService )
		{
			_savedDataService = savedDataService;
			_eventBusService = eventBusService;

			if( _savedDataService.IsDataLoaded )
			{
				InitStorageObject();
			}
			else
			{
				_eventBusService.Subscribe<SavedDataLoadedSignal>(OnSavedDataLoadedSignal);
			}
		}

		private void OnSavedDataLoadedSignal( SavedDataLoadedSignal signal )
		{
			_eventBusService.Unsubscribe<SavedDataLoadedSignal>(OnSavedDataLoadedSignal);
			InitStorageObject();
		}

		private LanguageStorageObject _storageObject;
		private void InitStorageObject()
		{
			_storageObject = new LanguageStorageObject(LanguageIndexFromSystem, _savedDataService);
		}


		

		private LanguageType LanguageIndexFromSystem => Application.systemLanguage switch
		{
			SystemLanguage.Russian => LanguageType.RU,
			SystemLanguage.English => LanguageType.EN,
			//SystemLanguage.German => Languages.DE,
			//SystemLanguage.Spanish => Languages.ES,
			//SystemLanguage.Italian => Languages.IT,
			//SystemLanguage.French => Languages.FR,
			//SystemLanguage.Portuguese => Languages.PT,
			//SystemLanguage.Turkish => Languages.TR,
			_ => LanguageType.RU
		};

		public LanguageType CurrLanguage
		{
			get
			{
				if( _savedDataService.IsDataLoaded )
				{
					return _storageObject.CurrLanguage;
				}
				else 
				{
					return LanguageIndexFromSystem;
				}
			}
			set 
			{
				_storageObject.CurrLanguage = value;
				_eventBusService.Invoke(new LanguageChangedSignal(_storageObject.CurrLanguage));
			} 
		}
	}
}
