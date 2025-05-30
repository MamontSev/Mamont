using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Mamont.Static;

#if UNITY_EDITOR
using Unity.EditorCoroutines.Editor;

using UnityEditor;
using UnityEditor.SceneManagement;
#endif

using UnityEngine;
using UnityEngine.Networking;

namespace Mamont.Data.DataConfig.Text
{

	[CreateAssetMenu(menuName = "Data/Text" , fileName = "TextConfigDataTable.asset")]
	public class TextConfigDataTable:ScriptableObject, ITextConfigDataTable
	{
		[Serializable]
		class SheetItem
		{
			public SheetItem( TextDataType textType )
			{
				TextType = textType;
			}
			public TextDataType TextType;
			public long GridId;
			public TextAsset TextAsset;
		}

		public TextAsset GetTextAsset( TextDataType type )
		{
			return _sheetItems.Find(x => x.TextType == type).TextAsset;
		}
		private string SheetId => "";

		public UnityEngine.Object SaveFolder;

		[SerializeField]
		private List<SheetItem> _sheetItems = new();
		

#if UNITY_EDITOR

		public void CheckSheetItems()
		{
			foreach( TextDataType type in Enum.GetValues(typeof(TextDataType)) )
			{
				bool contain = false;
				foreach( var item in _sheetItems )
				{
					if( item.TextType == type )
					{
						contain = true;
						break;
					}
				}
				if( contain == false )
				{
					_sheetItems.Add(new SheetItem(type));
				}
			}
			for( int i = _sheetItems.Count - 1; i >= 0; i-- )
			{
				if( Enum.IsDefined(typeof(TextDataType) , _sheetItems[i].TextType) == false )
				{
					_sheetItems.RemoveAt(i);
				}
			}
			_sheetItems = _sheetItems.GroupBy(x => x.TextType).Select(x => x.First()).ToList();
			_sheetItems = _sheetItems.OrderBy(x => x.TextType).ToList();
		}

		public void Load()
		{
			EditorCoroutineUtility.StartCoroutineOwnerless(DownloadGoogleSheetsCoroutine());
		}

		private IEnumerator DownloadGoogleSheetsCoroutine( )
		{

			if( SaveFolder == null )
			{
				EditorUtility.DisplayDialog("Error" , "Save Folder is not set." , "OK");
				yield break;
			}


			for( var i = 0; i < _sheetItems.Count; i++ )
			{
				SheetItem sheet = _sheetItems[i];
				var url = $@"https://docs.google.com/spreadsheet/ccc?key={SheetId}&usp=sharing&output=csv&id=KEY&gid={sheet.GridId}";

				Debug.Log($"Downloading <color=grey>{url}</color>");
				var request = UnityWebRequest.Get(url);
				var progress = (float)( i + 1 ) / _sheetItems.Count;

				if( EditorUtility.DisplayCancelableProgressBar("Downloading sheets..." , $"[{(int)( 100 * progress )}%] [{i + 1}/{_sheetItems.Count}] Downloading {sheet.TextType}..." , progress) )
				{
					yield break;
				}

				yield return request.SendWebRequest();

				var error = request.error ?? ( request.downloadHandler.text.Contains("signin/identifier") ? "It seems that access to this document is denied." : null );

				if( string.IsNullOrEmpty(error) )
				{
					var path = Path.Combine(AssetDatabase.GetAssetPath(SaveFolder) , sheet.TextType + ".csv");

					File.WriteAllBytes(path , request.downloadHandler.data);
					AssetDatabase.Refresh();
					_sheetItems[i].TextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
					EditorUtility.SetDirty(this);
					Debug.LogFormat($"Sheet <color=yellow>{sheet.TextType}</color> ({sheet.GridId}) saved to <color=grey>{path}</color>");
				}
				else
				{
					EditorUtility.ClearProgressBar();
					EditorUtility.DisplayDialog("Error" , error.Contains("404") ? "Table Id is wrong!" : error , "OK");

					yield break;
				}
			}
			yield return null;
			AssetDatabase.Refresh();
			EditorUtility.ClearProgressBar();
		}



		[CustomEditor(typeof(TextConfigDataTable))]
		class TextConfigDataTableCustomizer:Editor
		{
			public override void OnInspectorGUI()
			{
				if( GUI.changed )
				{
					EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
				}
				DrawDefaultInspector();//отрисовка содержимого инспектора по умолчанию
				TextConfigDataTable mt = (TextConfigDataTable)target;
				Undo.RegisterCompleteObjectUndo(mt , "Player name change");

                if ( GUILayout.Button("Check Sheet Items" , GUILayout.ExpandWidth(false)) )
				{
					mt.CheckSheetItems();
				}
				if( GUILayout.Button("Load" , GUILayout.ExpandWidth(false)) )
				{
					mt.Load();
				}
			}
		}
#endif

	}
}
