using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Mamont.Data.DataControl.HeroInventory;
using Mamont.Data.DataControl.Language;
using Mamont.Log;

namespace Mamont.Data.DataConfig.Text
{
	public class TextConfigData:ITextConfigData
	{
		private readonly ITextConfigDataTable _textConfigDataTable;
		private readonly ILanguageControlService _languageService;
		private readonly ILogService _logService;
		public TextConfigData
		(
			ITextConfigDataTable _textConfigDataTable ,
			ILanguageControlService _languageService ,
			ILogService _logService
		)
		{
			this._textConfigDataTable = _textConfigDataTable;
			this._languageService = _languageService;
			this._logService = _logService;
			SetData();
			CheckData();
		}

		private readonly Dictionary<TextDataType , Dictionary<LanguageType , Dictionary<string , string>>> _dataDict = new();

		private void SetData()
		{
			_dataDict.Clear();

			foreach( TextDataType type in Enum.GetValues(typeof(TextDataType)) )
			{
				var textAsset = _textConfigDataTable.GetTextAsset(type);
				var lines = GetLines(textAsset.text);
				var languages = lines[0].Split(',').Select(i => i.Trim()).ToList();
				if( languages.Count != languages.Distinct().Count() )
				{
					string s = $"Duplicated languages found in `{type}`. This sheet is not loaded.";
					_logService.LogError(s);
					throw new Exception(s);
				}
				foreach( LanguageType lang in Enum.GetValues(typeof(LanguageType)) )
				{
					if( languages.Contains(lang.ToString()) == false )
					{
						_logService.LogError($"Not contain language `{type}`");
						break;
					}
				}
				_dataDict.Add(type , new());
				for( var i = 1; i < languages.Count; i++ )
				{
					bool complete = Enum.TryParse(languages[i] , out LanguageType langType);
					if( complete == false )
					{
						string s = $"Fail parse language `{type}`";
						_logService.LogError(s);
						throw new Exception(s);
					}
					_dataDict[type].Add(langType , new());
				}

				for( var i = 1; i < lines.Count; i++ )
				{
					var columns = GetColumns(lines[i]);
					var key = columns[0];

					for( var j = 1; j < languages.Count; j++ )
					{
						if( _dataDict[type][(LanguageType)j].ContainsKey(key) )
						{
							string s = $"Duplicate key in `{type}` key {key}";
							_logService.LogError(s);
							throw new Exception(s);
						}
						_dataDict[type][(LanguageType)j].Add(key , columns[j]);
					}
				}
				foreach( var item in _dataDict[type] )
				{
					foreach( var item1 in item.Value )
					{
						//_logService.Log($"{item.Key} {item1.Key} {item1.Value}");
					}
				}
			}
		}
		private List<string> GetLines( string text )
		{
			text = text.Replace("\r\n" , "\n").Replace("\"\"" , "[_quote_]");

			var matches = Regex.Matches(text , "\"[\\s\\S]+?\"");

			foreach( Match match in matches )
			{
				text = text.Replace(match.Value , match.Value.Replace("\"" , null).Replace("," , "[_comma_]").Replace("\n" , "[_newline_]"));
			}

			// Making uGUI line breaks to work in asian texts.
			text = text.Replace("。" , "。 ").Replace("、" , "、 ").Replace("：" , "： ").Replace("！" , "！ ").Replace("（" , " （").Replace("）" , "） ").Trim();

			return text.Split('\n').Where(i => i != "").ToList();
		}

		private List<string> GetColumns( string line )
		{
			return line.Split(',').Select(j => j.Trim()).Select(j => j.Replace("[_quote_]" , "\"").Replace("[_comma_]" , ",").Replace("[_newline_]" , "\n")).ToList();
		}


		private void CheckData()
		{
			foreach( TextDataType type in Enum.GetValues(typeof(TextDataType)) )
			{
				int textCount = _dataDict[type][LanguageType.RU].Count;
				bool complete = type switch
				{
					TextDataType.TextTypeGeneral => ValidateEnum(typeof(TextTypeGeneral) , textCount),
					TextDataType.TextTypeMainMenuView => ValidateEnum(typeof(TextTypeMainMenuView) , textCount),
					_ => throw new NotImplementedException(),
				};
				if( complete == false )
				{
					throw new NotImplementedException($"Text data of type '{type}' mismatch value count");
				}
			}
		}

		private bool ValidateEnum( Type enumType , int textCount )
		{
			int count = Enum.GetNames(enumType).Length;
			return count == textCount;
		}

		private bool ValidateHeroInventoryItemName( int textCount )
		{
			int typeCount = Enum.GetNames(typeof(HeroInventoryItemType)).Length;
			int rarityCount = Enum.GetNames(typeof(HeroInventoryItemRarity)).Length;
			int count = typeCount * rarityCount;
			return count == textCount;
		}


		private string FingText( TextDataType type , int index )
		{
			try
			{
				return _dataDict[type][_languageService.CurrLanguage][index.ToString()];
			}
			catch( Exception ex )
			{
				_logService.LogError($"Fail to find text {type} {_languageService.CurrLanguage} {index}");
				return "";
			}
		}
		private string FingText( TextDataType type , int index , params object[] args )
		{
			string s = FingText(TextDataType.TextTypeGeneral , (int)type);
			if( s == "" )
			{
				return s;
			}
			return string.Format(s , args);
		}

		public string GetText( TextTypeGeneral type )
			=> FingText(TextDataType.TextTypeGeneral , (int)type);
		public string GetText( TextTypeGeneral type , params object[] args )
			=> FingText(TextDataType.TextTypeGeneral , (int)type , args);

		public string GetText( TextTypeMainMenuView type )
			=> FingText(TextDataType.TextTypeGeneral , (int)type);
		public string GetText( TextTypeMainMenuView type , params object[] args )
			=> FingText(TextDataType.TextTypeGeneral , (int)type , args);

	}
}
