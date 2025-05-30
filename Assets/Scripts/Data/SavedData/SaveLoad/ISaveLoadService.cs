using System;

namespace Mamont.Data.SaveData.SaveLoad
{
	public interface ISaveLoadService
	{
		void Save( string key , object data );

		void Load<T>( string key , Action<T> onComplete, Action onNoExistData, Action<string> onFailLoad );

	}
}

