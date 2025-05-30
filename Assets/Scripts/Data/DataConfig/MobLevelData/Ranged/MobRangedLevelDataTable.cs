using System;
using System.Collections.Generic;

using Mamont.Static;

using Manmont.Tools;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

using UnityEngine;

namespace Mamont.Data.DataConfig.Mob
{
	[CreateAssetMenu(menuName = "Data/Mob/MobRangedLevelDataTable" , fileName = "MobRangedLevelDataTable.asset")]
	public class MobRangedLevelDataTable:ScriptableObject
	{
		private string SheetId => GoogleSheetGridIds.SheetId;
		private string GridId => "302436347";
#if UNITY_EDITOR
		public bool Sync()
		{

			try
			{
				ReadGoogleSheetsData.FillData<MobRangedLevelDataTableRow>(SheetId , GridId , list =>
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
		private List<MobRangedLevelDataTableRow> dataList = new List<MobRangedLevelDataTableRow>();

		public int LevelCount => dataList.Count;

		public float Health( int level ) => dataList[level].Health;
		public float DamageMin( int level ) => dataList[level].DamageMin;
		public float DamageMax( int level ) => dataList[level].DamageMax;
		public float GroupAttack( int level ) => dataList[level].GroupAttack;
		public float GroupAttackRadius( int level ) => dataList[level].GroupAttackRadius;
		public float CritDamage( int level ) => dataList[level].CritDamage;
		public float CritChanse( int level ) => dataList[level].CritChanse;
		public float BeatInterval( int level ) => dataList[level].BeatInterval;
		public float AggresionRadius( int level ) => dataList[level].AggresionRadius;


		[Serializable]
		class MobRangedLevelDataTableRow
		{
			public int Level;
			public float Health;
			public float DamageMin;
			public float DamageMax;
			public float GroupAttack;
			public float CritDamage;
			public float CritChanse;
			public float AggresionRadius;
			public float GroupAttackRadius;
			public float BeatInterval;
		}

#if UNITY_EDITOR
		[CustomEditor(typeof(MobRangedLevelDataTable))]
		class MobFarFightLevelDataTableCustomizer:Editor
		{
			public override void OnInspectorGUI()// типа Update()
			{
				if( GUI.changed )
				{
					EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
				}
				DrawDefaultInspector();//отрисовка содержимого инспектора по умолчанию
				MobRangedLevelDataTable mt = (MobRangedLevelDataTable)target;
				Undo.RegisterCompleteObjectUndo(mt , "Player name change");


				if( GUILayout.Button("Sync" , GUILayout.ExpandWidth(false)) )
				{
					if( !mt.Sync() )
					{
						EditorUtility.DisplayDialog("Fail" , "Fail Load MobRangedLevelDataTable" , "OK" , "OK");
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
					EditorGUILayout.LabelField("GroupAttack" , GUILayout.Width(width));
					EditorGUILayout.LabelField("GroupAttackRadius" , GUILayout.Width(width));
					EditorGUILayout.LabelField("CritDamage" , GUILayout.Width(width));
					EditorGUILayout.LabelField("CritChanse" , GUILayout.Width(width));
					EditorGUILayout.LabelField("BeatInterval" , GUILayout.Width(width));
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
						EditorGUILayout.LabelField(mt.GroupAttack(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.GroupAttackRadius(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.CritDamage(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.CritChanse(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.BeatInterval(i).ToString() , GUILayout.Width(width));
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

