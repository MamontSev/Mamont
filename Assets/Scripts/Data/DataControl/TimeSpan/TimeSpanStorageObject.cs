
using System;
using System.Collections.Generic;

using Mamont.Data.SaveData;

using Newtonsoft.Json.Linq;

namespace Mamont.Data.DataControl.TimeSpanControl
{
	public interface ITimeSpanStorageObject
	{
		bool IsWorking( TimeSpanType type );
		void Stop( TimeSpanType type );
		long GetStartTime( TimeSpanType type );
		void Start( TimeSpanType type , long value );
	}
	public class TimeSpanStorageObject:IStorageObject, ITimeSpanStorageObject
	{
		public TimeSpanStorageObject( ISavedDataService _savedService )
		{
			foreach( TimeSpanType type in Enum.GetValues(typeof(TimeSpanType)) )
			{
				_startTimeDict.Add(type , 0);
				_isWorkDict.Add(type , false);
			}
			_savedService.InitBaseStorageObj(this);
			
		}

		public string GetKey() => "TimeSpanStorageObject";
		public Dictionary<string , object> GetSavedData()
		{
			Dictionary<string , object> data = new();
			data.Add(SavedObjNames.StartTimeDict.ToString() , _startTimeDict);
			data.Add(SavedObjNames.IsWorkDict.ToString() , _isWorkDict);
			return data;
		}
		public void SetSavedData( Dictionary<string , object> data )
		{
			foreach( KeyValuePair<string , object> obj in data )
			{
				if( obj.Key == SavedObjNames.StartTimeDict.ToString() )
				{
					Dictionary<string , long> temp = JObject.FromObject(obj.Value).ToObject<Dictionary<string , long>>();
					foreach( KeyValuePair<string , long> kv in temp )
					{
						bool b = Enum.TryParse<TimeSpanType>(kv.Key , out TimeSpanType type);
						if( b )
						{
							_startTimeDict[type] = kv.Value;
						}
					}
				}
				else if( obj.Key == SavedObjNames.IsWorkDict.ToString() )
				{
					Dictionary<string , bool> temp = JObject.FromObject(obj.Value).ToObject<Dictionary<string , bool>>();
					foreach( KeyValuePair<string , bool> kv in temp )
					{
						bool b = Enum.TryParse<TimeSpanType>(kv.Key , out TimeSpanType type);
						if( b )
						{
							_isWorkDict[type] = kv.Value;
						}
					}
				}
			}
		}


		private Dictionary<TimeSpanType , long> _startTimeDict = new();
		private Dictionary<TimeSpanType , bool> _isWorkDict = new();

		public bool IsWorking( TimeSpanType type )
		{
			if( _isWorkDict.ContainsKey(type) == false )
			{
				_isWorkDict[type] = false;
			}
			return _isWorkDict[type];
		}
	
		public long GetStartTime( TimeSpanType type )
		{
			if( _startTimeDict.ContainsKey(type) == false )
			{
				_startTimeDict[type] = 0;
			}
			return _startTimeDict[type];
		}
		public void Start( TimeSpanType type , long value )
		{
			_startTimeDict[type] = value;
			_isWorkDict[type] = true;
		}
		public void Stop( TimeSpanType type )
		{
			_isWorkDict[type] = false;
		}

		enum SavedObjNames
		{
			StartTimeDict,
			IsWorkDict
		}

	}
}

