
using System;
using System.Collections.Generic;
using System.Data;

using UnityEngine;

namespace Mamont.Data.DataConfig.Mob
{
	public class MobKamikazeDataConfig:IMobKamikazeDataConfig
	{
		private readonly MobKamikazeLevelDataTable _dataTable;
		public MobKamikazeDataConfig( MobKamikazeLevelDataTable _dataTable )
		{
			this._dataTable = _dataTable;
			Init();
		}

		public MobKamikazeLevelData GetMobLevelData( int level )
		{
			if( level < 0 )
			{
				return _mobLevelDataList[0];
			}
			if( level > MaxMobLevel )
			{
				return _mobLevelDataList[MaxMobLevel];
			}
			return _mobLevelDataList[level];
		}
		public int MaxMobLevel => _mobLevelDataList.Count - 1;

		private List<MobKamikazeLevelData> _mobLevelDataList = new();
		private void Init()
		{
			if( _dataTable == null )
			{
				throw new Exception("Error Load MobKamikazeLevelDataTable");
			}
			for( int i = 0; i < _dataTable.LevelCount; i++ )
			{
				MobKamikazeLevelData temp = new MobKamikazeLevelData
				(
					_dataTable.Health(i) ,
					_dataTable.DamageMin(i) ,
					_dataTable.DamageMax(i) ,
					_dataTable.WalkSpeed(i) ,
					_dataTable.AttackRadius(i) ,
					_dataTable.AttackCenter(i) ,
					_dataTable.AttackEdge(i) ,
					_dataTable.CritDamage(i) ,
					_dataTable.CritChanse(i) ,
					_dataTable.AttackDistance(i) ,
					_dataTable.AggresionRadius(i) 
				);
				_mobLevelDataList.Add(temp);
			}
		}
	}
}


