using System;
using System.Collections.Generic;

using UnityEngine;

namespace Mamont.Data.DataConfig.Mob
{
	public class MobMeleeLevelDataConfig :IMobMeleeLevelDataConfig
	{
		private readonly MobMeleeLevelDataTable _dataTable;
		public MobMeleeLevelDataConfig( MobMeleeLevelDataTable _dataTable )
		{
			this._dataTable = _dataTable;
			Init();
		}

		public MobMeleeFightLevelData GetMobLevelData( int level )
		{
			if( level < 0  )
			{
				return _mobLevelDataList[0];
			}
			if(  level > MaxMobLevel )
			{
				return _mobLevelDataList[MaxMobLevel];
			}
			return _mobLevelDataList[level];
		}
		public int MaxMobLevel => _mobLevelDataList.Count - 1;

		private List<MobMeleeFightLevelData> _mobLevelDataList = new();
		private void Init()
		{
			if( _dataTable == null )
			{
				throw new Exception("Error Load MobMeleeLevelDataTable");
			}
			for( int i = 0; i < _dataTable.LevelCount; i++ )
			{
				MobMeleeFightLevelData temp = new MobMeleeFightLevelData
				(
					_dataTable.Health(i) ,
					_dataTable.DamageMin(i) ,
					_dataTable.DamageMax(i) ,
					_dataTable.WalkSpeed(i) ,
					_dataTable.GroupAttack(i) ,
					_dataTable.GroupAttackRadius(i) ,
					_dataTable.CritDamage(i) ,
					_dataTable.CritChanse(i) ,
					_dataTable.BeatInterval(i) ,
					_dataTable.AttackDistance(i) ,
					_dataTable.AggresionRadius(i)
				);
				_mobLevelDataList.Add(temp);
			}
		}
	}
}
