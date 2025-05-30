
using System;
using System.Collections.Generic;

using Mamont.Data.DataConfig.TimeSpan;
using Mamont.Data.DataControl.TimeSpanControl;

using UnityEngine;

namespace Mamont.Data.DataConfig
{
	public interface ITimeSpanDataConfig
	{
		long GetAwaitSeconds( TimeSpanType type );
	}
	public class TimeSpanDataConfig:ITimeSpanDataConfig
	{
		private readonly TimeSpanDataTable _dataTable;
		public TimeSpanDataConfig( TimeSpanDataTable _dataTable )
		{
			this._dataTable = _dataTable;
			Init();
		}

	
		public long GetAwaitSeconds( TimeSpanType type )
		{
			return _awaitTimeDict[type];
		}

	
		private Dictionary<TimeSpanType , long> _awaitTimeDict = new();
		private void Init()
		{
			//string fullPath = "Data/TimeSpanDataTable";
			//TimeSpanDataTable dataTable = Resources.Load<TimeSpanDataTable>(fullPath);
			if( _dataTable == null )
			{
				throw new Exception("Error Load TimeSpanDataTable");
			}
			if( _dataTable.ItemList.Count != Enum.GetValues(typeof(TimeSpanType)).Length )
			{
				throw new Exception("Error  TimeSpanDataTable not contain any");
			}

			foreach( TimeSpanType type in Enum.GetValues(typeof(TimeSpanType)) )
			{
				foreach( var item in _dataTable.ItemList )
				{
					if( item.SelfType == type )
					{
						_awaitTimeDict.Add(type , item.SecondsAwait);
					}
				}
			}
		}
	}
}




