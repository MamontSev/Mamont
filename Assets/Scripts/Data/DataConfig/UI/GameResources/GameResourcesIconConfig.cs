using System;
using System.Collections.Generic;

using Mamont.Data.DataControl.Resources;

using UnityEngine;

namespace Mamont.Data.DataConfig.UI
{
	public interface IGameResourcesIconConfig
	{
		Sprite ResourceSpriteLarge( GameResourceType gameResourceType );
		Sprite ResourceSpriteSmall( GameResourceType gameResourceType );
	}
	public class GameResourcesIconConfig:IGameResourcesIconConfig
	{
		private readonly GameResourcesIconConfigTable _dataTable;
		public GameResourcesIconConfig( GameResourcesIconConfigTable _dataTable )
		{
			this._dataTable = _dataTable;
			Init();
		}

		private Dictionary<GameResourceType , Sprite> _resourcesSmallDict = new();
		private Dictionary<GameResourceType , Sprite> _resourcesLargeDict = new();

		public Sprite ResourceSpriteSmall( GameResourceType gameResourceType )
		{
			return _resourcesSmallDict[gameResourceType];
		}

		public Sprite ResourceSpriteLarge( GameResourceType gameResourceType )
		{
			return _resourcesLargeDict[gameResourceType];
		}


		private void Init()
		{
			if( _dataTable == null )
			{
				throw new Exception("Error Load GameResourcesIcon");
			}

			CheckRes(_dataTable);
			InitRes(_dataTable);

		}
		private void InitRes( GameResourcesIconConfigTable data )
		{
			foreach( var item in data.ItemList )
			{
				_resourcesSmallDict.Add(item.SelfType , item.SpriteAr[0]);
				_resourcesLargeDict.Add(item.SelfType , item.SpriteAr[1]);
			}
		}

		private void CheckRes( GameResourcesIconConfigTable data )
		{
			foreach( GameResourceType type in Enum.GetValues(typeof(GameResourceType)) )
			{
				bool contain = false;
				foreach( var item in data.ItemList )
				{
					if( item.SelfType == type )
					{
						contain = true;
						break;
					}
				}
				if( contain == false )
				{
					throw new Exception($"GameResourcesIconConfigTable not contain {type}");
				}
			}
			foreach( var item in data.ItemList )
			{
				if( item.SpriteAr.Length < 2 )
				{
					throw new Exception($"GameResourcesIconConfigTable not contain any {item.SelfType}");
				}
                foreach (var item1 in item.SpriteAr)
                {
					if( item1 == null )
					{
						throw new Exception($"GameResourcesIconConfigTable sprite is null");
					}
				}
            }
		}

	
	}
}

