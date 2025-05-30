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
	[CreateAssetMenu(menuName = "Data/Mob/MobMeleeLevelDataTable" , fileName = "MobMeleeLevelDataTable.asset")]
	public class MobMeleeLevelDataTable:ScriptableObject
	{
		private string SheetId => "";
		private string GridId => "";

#if UNITY_EDITOR
		public bool Sync()
		{
			try
			{
				ReadGoogleSheetsData.FillData<MobMeleeLevelDataTableRow>(SheetId , GridId , list =>
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
		private List<MobMeleeLevelDataTableRow> dataList = new List<MobMeleeLevelDataTableRow>();

		public int LevelCount => dataList.Count;

		public float Health( int level )  => dataList[level].Health;
		public float DamageMin( int level ) => dataList[level].DamageMin;
		public float DamageMax( int level ) => dataList[level].DamageMax;
		public float WalkSpeed( int level ) => dataList[level].WalkSpeed;
		public float GroupAttack( int level ) => dataList[level].GroupAttack;
		public float GroupAttackRadius( int level ) => dataList[level].GroupAttackRadius;
		public float CritDamage( int level ) => dataList[level].CritDamage;
		public float CritChanse( int level ) => dataList[level].CritChanse;
		public float BeatInterval( int level ) => dataList[level].BeatInterval;
		public float AttackDistance( int level ) => dataList[level].AttackDistance;
		public float AggresionRadius( int level ) => dataList[level].AggresionRadius;



		[Serializable]
		class MobMeleeLevelDataTableRow
		{
			public int Level;
			public float Health;
			public float DamageMin;
			public float DamageMax;
			public float WalkSpeed;
			public float GroupAttack;
			public float GroupAttackRadius;
			public float CritDamage;
			public float CritChanse;
			public float BeatInterval;
			public float AttackDistance;
			public float AggresionRadius;
		}

#if UNITY_EDITOR
		[CustomEditor(typeof(MobMeleeLevelDataTable))]
		class MobMeleeLevelDataTableCustomizer:Editor
		{
			public override void OnInspectorGUI()// типа Update()
			{
				if( GUI.changed )
				{
					EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
				}
				DrawDefaultInspector();//отрисовка содержимого инспектора по умолчанию
				MobMeleeLevelDataTable mt = (MobMeleeLevelDataTable)target;
				Undo.RegisterCompleteObjectUndo(mt , "Player name change");


				if( GUILayout.Button("Sync" , GUILayout.ExpandWidth(false)) )
				{
					if( !mt.Sync() )
					{
						EditorUtility.DisplayDialog("Fail" , "Fail Load MobMeleeLevelDataTable" , "OK" , "OK");
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
					EditorGUILayout.LabelField("GroupAttack" , GUILayout.Width(width));
					EditorGUILayout.LabelField("GroupAttackRadius" , GUILayout.Width(width));
					EditorGUILayout.LabelField("CritDamage" , GUILayout.Width(width));
					EditorGUILayout.LabelField("CritChanse" , GUILayout.Width(width));
					EditorGUILayout.LabelField("BeatInterval" , GUILayout.Width(width));
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
						EditorGUILayout.LabelField(mt.GroupAttack(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.GroupAttackRadius(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.CritDamage(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.CritChanse(i).ToString() , GUILayout.Width(width));
						EditorGUILayout.LabelField(mt.BeatInterval(i).ToString() , GUILayout.Width(width));
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

