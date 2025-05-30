
using System;
using System.Collections.Generic;

using UnityEngine;



using Mamont.Data.DataControl.TimeSpanControl;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
using UnityEditor;
#endif

namespace Mamont.Data.DataConfig.TimeSpan
{
	[CreateAssetMenu(menuName = "Data/TimeSpanDataTable" , fileName = "TimeSpanDataTable.asset")]
	public class TimeSpanDataTable:ScriptableObject
	{
		public List<RowItem> ItemList = new();

		[Serializable]
		public class RowItem
		{
			public RowItem( TimeSpanType _selfType )
			{
				this._selfType = _selfType;
				_secondsAwait = 10;
			}

			[SerializeField]
			private TimeSpanType _selfType;
			public TimeSpanType SelfType => _selfType;

			[SerializeField]
			private long _secondsAwait;
			public long SecondsAwait => _secondsAwait;
		}

#if UNITY_EDITOR

		public void CheckState()
		{
			foreach( TimeSpanType type in Enum.GetValues(typeof(TimeSpanType)) )
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

		[CustomEditor(typeof(TimeSpanDataTable))]
		class TimeSpanDataTableCustomizer:Editor
		{
			public override void OnInspectorGUI()// типа Update()
			{
				if( GUI.changed )
				{
					EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
				}
				EditorGUILayout.LabelField("TimeSpanType           SecondsAwait ");
				DrawDefaultInspector();//отрисовка содержимого инспектора по умолчанию
				TimeSpanDataTable mt = (TimeSpanDataTable)target;
				Undo.RegisterCompleteObjectUndo(mt , "Player name change");


				if( GUILayout.Button("CheckState" , GUILayout.ExpandWidth(false)) )
				{
					mt.CheckState();
				}

			}
		}

		[CustomPropertyDrawer(typeof(RowItem))]
		public class RowItemDrawer:PropertyDrawer
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

				width = 120.0f;
				Rect valueRect = new Rect(px , py , width , height);
				int k = property.FindPropertyRelative("_selfType").enumValueIndex;
				EditorGUI.LabelField(valueRect , $"{(TimeSpanType)k}");
				px += width;



				width = 120.0f;
				valueRect = new Rect(px , py , width , height);
				EditorGUI.PropertyField(valueRect , property.FindPropertyRelative("_secondsAwait") , GUIContent.none);
				px += width;
			}
		}


#endif
	}
}




