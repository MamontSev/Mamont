using System.Collections.Generic;

namespace Mamont.Data.SaveData
{
	public interface IStorageObject
	{
		string GetKey();
		Dictionary<string , object> GetSavedData();
		void SetSavedData( Dictionary<string , object> data);
	}

}
