using System;
using System.Collections.Generic;

using UnityEngine;

namespace Mamont.Data.DataConfig.Mob
{
	public class MobRangedLevelDataConfig:IMobRangedLevelDataConfig
	{
		private readonly MobRangedLevelDataTable _dataTable;
		public MobRangedLevelDataConfig( MobRangedLevelDataTable _dataTable )
		{
			this._dataTable = _dataTable;
			Init();
		}

		public MobRangedLevelData GetMobLevelData( int level )
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

		private readonly List<MobRangedLevelData> _mobLevelDataList = new();

		private void Init()
		{
			//string fullPath = "Data/Mob/MobRangedLevelDataTable";
			//MobRangedLevelDataTable _dataTable = Resources.Load<MobRangedLevelDataTable>(fullPath);
			if( _dataTable == null )
			{
				throw new Exception("Error Load MobRangedLevelDataTable");
			}
			for( int i = 0; i < _dataTable.LevelCount; i++ )
			{
				MobRangedLevelData temp = new MobRangedLevelData
				(
					_dataTable.Health(i) ,
					_dataTable.DamageMin(i) ,
					_dataTable.DamageMax(i) ,
					_dataTable.GroupAttack(i) ,
					_dataTable.GroupAttackRadius(i) ,
					_dataTable.CritDamage(i) ,
					_dataTable.CritChanse(i) ,
					_dataTable.BeatInterval(i) ,
					_dataTable.AggresionRadius(i) 
				);
				_mobLevelDataList.Add(temp);
			}
		}
	}
}
