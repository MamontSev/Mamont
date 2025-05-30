using System;
using System.IO;

using Mamont.Log;

using Newtonsoft.Json;

using UnityEngine;

namespace Mamont.Data.SaveData.SaveLoad
{
	public class SaveLoadServiceLocal:ISaveLoadService
	{
		private readonly ILogService _logService;

		public SaveLoadServiceLocal( ILogService _logService )
		{
			this._logService = _logService;
		}

		public void Load<T>( string key , Action<T> onComplete , Action onNoExistData, Action<string> onFailLoad )
		{
			if( File.Exists(FullPath(key)) )
			{
				try
				{
					string json = "";
					using( FileStream stream = new FileStream(FullPath(key) , FileMode.Open) )
					{
						using( StreamReader reader = new StreamReader(stream) )
						{
							json = reader.ReadToEnd();
						}
					}
					json = DescryptEncrypt(json);
					var data = JsonConvert.DeserializeObject<T>(json);
					onComplete.Invoke(data);
				}
				catch (Exception ex)
				{
					onFailLoad.Invoke(ex.Message);
				}
			}
			else
			{
				onNoExistData.Invoke();
			}
		}

		public void Save( string key , object data  )
		{
			try
			{
				string directory = Path.GetDirectoryName(FullPath(key));
				if( !Directory.Exists(directory) )
				{
					Directory.CreateDirectory(directory);
				}
				string json = JsonConvert.SerializeObject(data, Formatting.Indented);
				json = DescryptEncrypt(json);
				using FileStream stream = new FileStream(FullPath(key) , FileMode.Create);
				using StreamWriter writer = new StreamWriter(stream);
				writer.Write(json);
				_logService.Log("Save completed",LogPriority.Main);
			}
			catch( Exception ex )
			{
				_logService.LogError($"Fail save: {ex.Message} ");
			}
		}
		private string FullPath( string dataFileName ) => Path.Combine(Application.persistentDataPath , dataFileName);

		private string DescryptEncrypt( string s )
		{
			string str = "gamedata";
			string retVal = "";
			for( int i = 0; i < s.Length; i++ )
			{
				retVal += (char)( s[i] ^ str[i % str.Length] );
			}
			return retVal;
		}
	}
}
