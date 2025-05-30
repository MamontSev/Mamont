using System;
using System.Collections.Generic;

using Mamont.Data.SaveData;

using Newtonsoft.Json.Linq;

namespace Mamont.Data.DataControl.Resources
{
	public class ResourcesStorageObject:IStorageObject, IResourcesStorageObject
	{
		public ResourcesStorageObject( ISavedDataService _savedService )
		{
			foreach( GameResourceType type in Enum.GetValues(typeof(GameResourceType)) )
			{
				_resDict.Add(type , DefaultResValue(type));
			}

			_savedService.InitBaseStorageObj(this);

		}

		private float DefaultResValue( GameResourceType type ) => type switch
		{
			GameResourceType.Diamond => 0.0f,
			GameResourceType.Coin => 0.0f,
			GameResourceType.EnergyStart => 25.0f,
			_ => 0.0f
		};

		public string GetKey() => "ResourcesStorageObject";
		public Dictionary<string , object> GetSavedData()
		{
			Dictionary<string , object> data = new();
			data.Add(SavedObjNames.ResDict.ToString() , _resDict);
			//data.Add(SavedObjNames.MyList.ToString() , _myListExample);
			return data;
		}
		public void SetSavedData( Dictionary<string , object> data )
		{
			foreach( KeyValuePair<string , object> obj in data )
			{
				if( obj.Key == SavedObjNames.ResDict.ToString() )
				{
					Dictionary<string , float> temp = JObject.FromObject(obj.Value).ToObject<Dictionary<string , float>>();
					foreach( KeyValuePair<string , float> kv in temp )
					{
						bool b = Enum.TryParse<GameResourceType>(kv.Key , out GameResourceType type);
						if( b )
						{
							_resDict[type] = kv.Value;
						}
					}
				}
				//if( obj.Key == SavedObjNames.MyList.ToString() )
				//{
				//	_myListExample = ( (JArray)obj.Value ).ToObject<List<int>>();
				//}
			}
		}


		private Dictionary<GameResourceType , float> _resDict = new();
		public float GetResCount( GameResourceType type )
		{
			if( _resDict.ContainsKey(type) == false )
			{
				_resDict[type] = 0.0f;
			}
			return _resDict[type];
		}
		public void SetResCount( GameResourceType type , float value ) => _resDict[type] = value;

		private List<int> _myListExample = new();

		enum SavedObjNames
		{
			ResDict,
			//MyList
		}

	}
}
