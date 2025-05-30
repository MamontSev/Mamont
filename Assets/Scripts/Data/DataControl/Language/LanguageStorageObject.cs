using System;
using System.Collections.Generic;

using Mamont.Data.SaveData;

namespace Mamont.Data.DataControl.Language.Storage
{
	public class LanguageStorageObject:IStorageObject
	{
		public LanguageStorageObject( LanguageType defaulLangugae, ISavedDataService _savedService )
		{
			this.defaulLangugae = defaulLangugae;
			CurrLanguage = this.defaulLangugae;
			_savedService.InitBaseStorageObj(this);
		}

		private LanguageType defaulLangugae;

		public string GetKey() => "LanguageStorageObject";
		public Dictionary<string , object> GetSavedData()
		{
			Dictionary<string , object> data = new();
			data.Add(SavedObjNames.LanguageKey.ToString() , CurrLanguage.ToString());
			return data;
		}
		public void SetSavedData( Dictionary<string , object> data )
		{
			foreach( KeyValuePair<string , object> obj in data )
			{
				if( obj.Key == SavedObjNames.LanguageKey.ToString() )
				{
					bool complete = Enum.TryParse<LanguageType>(obj.Value.ToString() , out LanguageType lang);
					if( complete )
					{
						CurrLanguage = lang;
					}
					else
					{
						CurrLanguage = defaulLangugae;
					}
				}
			}
		}

		public LanguageType CurrLanguage = LanguageType.RU;

		enum SavedObjNames
		{
			LanguageKey
		}

	}
}

