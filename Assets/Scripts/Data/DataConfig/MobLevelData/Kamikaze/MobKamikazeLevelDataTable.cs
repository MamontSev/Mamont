
using System;
using System.Collections.Generic;

using Manmont.Tools;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

using UnityEngine;

namespace Mamont.Data.DataConfig.Mob
{
	[CreateAssetMenu(menuName = "Data/Mob/MobKamikazeLevelDataTable" , fileName = "MobKamikazeLevelDataTable.asset")]
	public class MobKamikazeLevelDataTable:ScriptableObject
	{
		private string SheetId => "";
		private string GridId => "";
#if UNITY_EDITOR
		public bool Sync()
		{
			try
			{
				ReadGoogleSheetsData.FillData<MobKamikazeDataTableRow>(SheetId , GridId , list =>
				{
					dataList = list;
					ReadGoogleSheetsData.SetDirty(this);
				});
				Debug.Log($"Sync {name}");
				return true;
			}
			catch( Exception ex )
			{
				return false;
			}
		}
#endif
		[SerializeField]
		private List<MobKamikazeDataTableRow> dataList = new List<MobKamikazeDataTableRow>();

		public int LevelCount => dataList.Count;

		public float Health( int level ) => dataList[level].Health;
		public float DamageMin( int level ) => dataList[level].DamageMin;
		public float DamageMax( int level ) => dataList[level].DamageMax;
		public float WalkSpeed( int level ) => dataList[level].WalkSpeed;
		public float AttackRadius( int level ) => dataList[level].AttackRadius;
		public float AttackCenter( int level ) => dataList[level].AttackCenter;
		public float AttackEdge( int level ) => dataList[level].AttackEdge;
		public float CritDamage( int level ) => dataList[level].CritDamage;
		public float CritChanse( int level ) => dataList[level].CritChanse;
		public float AttackDistance( int level ) => dataList[level].AttackDistance;
		public float AggresionRadius( int level ) => dataList[level].AggresionRadius;



		[Serializable]
		class MobKamikazeDataTableRow
		{
			public int Level;
			public float Health;
			public float DamageMin;
			public float DamageMax;
			public float WalkSpeed;
			public float AttackRadius;
			public float AttackCenter;
			public float AttackEdge;
			public float CritDamage;
			public float CritChanse;
			public float AttackDistance;
			public float AggresionRadius;
		}

#if UNITY_EDITOR
		[CustomEditor(typeof(MobKamikazeLevelDataTable))]
		class MobKamikazeLevelDataTableCustomizer:Editor
		{
			public override void OnInspectorGUI()// типа Update()
			{
				if( GUI.changed )
				{
					EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
				}
				DrawDefaultInspector();//отрисовка содержимого инспектора по умолчанию
				MobKamikazeLevelDataTable mt = (MobKamikazeLevelDataTable)target;
				Undo.RegisterCompleteObjectUndo(mt , "Player name change");


				if( GUILayout.Button("Sync" , GUILayout.ExpandWidth(false)) )
				{
					if( !mt.Sync() )
					{
						EditorUtility.DisplayDialog("Fail" , "Fail Load MobKamikazeLevelDataTable" , "OK" , "OK");
					}
				}
				if( mt.LevelCount > 0 )
				{
					EditorGUILayout.Space();
					float width = 90;

					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("Level" , GUILayout.Width(width));
					EditorGUILayout.LabelField("Health" , GUILayout.Width(width));
					EditorGUILayout.LabelField("DamageMin" , GUILayout.Width(width));
					EditorGUILayout.LabelField("DamageMax" , GUILayout.Width(width));
					EditorGUILayout.LabelField("WalkSpeed" , GUILayout.Width(width));
					EditorGUILayout.LabelField("AttackRadius" , GUILayout.Width(width));
					EditorGUILayout.LabelField("AttackCenter" , GUILayout.Width(width));
					EditorGUILayout.LabelField("AttackEdge" , GUILayout.Width(width));
					EditorGUILayout.LabelField("CritDamage" , GUILayout.Width(width));
					EditorGUILayout.LabelField("CritChanse" , GUILayout.Width(width));
					EditorGUILayout.LabelField("AttackDistance" , GUILayout.Width(width));
					EditorGUILayout.LabelField("AggresionRadius" , GUILayout.Width(width));

					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginVertical();
					for( int i = 0; i < mt.LevelCount; i++ )
					{
						EditorGUILayout.BeginHorizontal();

						EditorGUILayout.LabelField(i.ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.Health(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.DamageMin(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.DamageMax(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.WalkSpeed(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.AttackRadius(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.AttackCenter(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.AttackEdge(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.CritDamage(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.CritChanse(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.AttackDistance(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.AggresionRadius(i).ToString() , GUILayout.Width(width));

						EditorGUILayout.EndHorizontal();

						EditorGUILayout.Space();
					}
					EditorGUILayout.EndVertical();
				}
			}
		}
#endif
	}







}



