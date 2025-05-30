
using System;
using System.Collections.Generic;

using UnityEngine;

using Mamont.Data.DataControl.Resources;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Mamont.Data.DataConfig.UI
{
	[CreateAssetMenu(menuName = "Data/UI/GameResourcesIcon" , fileName = "GameResourcesIcon.asset")]
	public class GameResourcesIconConfigTable:ScriptableObject
	{
		public List<RowItem> ItemList = new();

		[Serializable]
		public class RowItem
		{
			public RowItem( GameResourceType _selfType )
			{
				this._selfType = _selfType;
				_spriteAr = new Sprite[2];
			}

			[SerializeField]
			private GameResourceType _selfType;
			public GameResourceType SelfType => _selfType;

			[SerializeField]
			private Sprite[] _spriteAr;

			public Sprite[] SpriteAr => _spriteAr;
		}

#if UNITY_EDITOR

		public void CheckState()
		{
			foreach( GameResourceType type in Enum.GetValues(typeof(GameResourceType)) )
			{
				bool contain = false;
				foreach( var item in ItemList )
				{
					if( item.SelfType == type )
					{
						contain = true;
						break;
					}
				}
				if( contain == false )
				{
					ItemList.Add(new RowItem(type));
				}
			}

		}



		[CustomPropertyDrawer(typeof(RowItem))]
		private class RowItemDrawer:PropertyDrawer
		{

			// Draw the property inside the given rect
			public override void OnGUI( Rect position , SerializedProperty property , GUIContent label )
			{
				float x = position.x;
				float y = position.y;
				float w = position.width;
				float h = position.height;
				Rect r = new Rect(x , y , w , h);

				EditorGUI.indentLevel = 1;

				float px = position.x;
				float py = position.y;
				float width = 100.0f;
				float height = position.height;

				width = 150.0f;
				Rect valueRect = new Rect(px , py , width , height);
				int k = property.FindPropertyRelative("_selfType").enumValueIndex;
				EditorGUI.LabelField(valueRect , $"{(GameResourceType)k}");
				px += width;

				int count =  property.FindPropertyRelative("_spriteAr").arraySize;
				Debug.Log(count);
				for( int i = 0; i < count; i++ )
				{
					width = 120.0f;
					valueRect = new Rect(px , py , width , height);
					EditorGUI.PropertyField(valueRect , property.FindPropertyRelative("_spriteAr").GetArrayElementAtIndex(i) , GUIContent.none);
					px += width;
				}

			}
		}

		[CustomEditor(typeof(GameResourcesIconConfigTable))]
		class GameResourcesIconConfigTableCustomizer:Editor
		{
			public override void OnInspectorGUI()
			{
				if( GUI.changed )
				{
					EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
				}
				EditorGUILayout.LabelField("GameResourceType                      Icon small                         Icon big ");
				DrawDefaultInspector();//отрисовка содержимого инспектора по умолчанию
				GameResourcesIconConfigTable mt = (GameResourcesIconConfigTable)target;
				Undo.RegisterCompleteObjectUndo(mt , "Player name change");


				if( GUILayout.Button("CheckState" , GUILayout.ExpandWidth(false)) )
				{
					mt.CheckState();
				}

			}
		}


#endif
	}
}





