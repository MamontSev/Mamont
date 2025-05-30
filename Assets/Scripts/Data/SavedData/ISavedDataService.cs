using System;

namespace Mamont.Data.SaveData
{
	public interface ISavedDataService
	{
		bool IsDataLoaded
		{
			get;
		}
		void LoadData( Action onComplete );
		void SaveGame();
		int GameStartCounter
		{
			get; set;
		}

		bool UseVibration
		{
			get;
			set;
		}

		void InitBaseStorageObj( IStorageObject storageObject );

	}
}

