using System;
using System.Collections.Generic;

namespace Mamont.Data.SaveData
{
	[Serializable]
	public class SavedData
	{
		public SavedData()
		{

		}

		public int GameStartCounter = 0;

		public bool UseVibration = true;

		public Dictionary<string , Dictionary<string , object>> BaseStorage = new();
	}
}
