using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Manmont.Tools
{
	public static class ValueConvertor
	{
		public static string DigitToString( this float val )
		{
			return Convert((double)val);
		}
		public static string DigitToString( this int val )
		{
			return Convert((double)val);
		}
		public static string DigitToString( this double val )
		{
			return Convert(val);
		}

		private static string Convert( double val )
		{
			string s;
			// к нулю близко в вычислениях получается типа 	1E-7f
			if( val < 0.01 )
			{
				s = "0";
				return s;
			}
			if( val < 1.0 )
			{
				s = subStr(val.ToString() , 4);
				return s;
			}
			// десять
			if( val < 1E+1 )
			{
				s = subStr(val.ToString() , 4);
				return s;
			}
			// тысяча
			if( val < 1E+3 )
			{
				s = subStr(val.ToString() , 5);
				return s;
			}
			// десять тысяч
			if( val < 1E+4 )
			{
				s = subStr(val.ToString() , 4);
				return s;
			}

			// сто тысяч
			if( val < 1E+5 )
			{
				val /= 1E+3;
				s = subStr(val.ToString() , 4);
				s = string.Concat(s , "k");
				return s;
			}
			// миллион
			if( val < 1E+6 )
			{
				val /= 1E+3;
				s = subStr(val.ToString() , 3);
				s = string.Concat(s , "K");
				return s;
			}

			// сто миллионов
			if( val < 1E+8 )
			{
				val /= 1E+6;
				s = subStr(val.ToString() , 4);
				s = string.Concat(s , "M");
				return s;
			}
			// миллиард
			if( val < 1E+9 )
			{
				val /= 1E+6;
				s = subStr(val.ToString() , 3);
				s = string.Concat(s , "M");
				return s;
			}

			// сто миллиардов
			if( val < 1E+11 )
			{
				val /= 1E+9;
				s = subStr(val.ToString() , 4);
				s = string.Concat(s , "B");
				return s;
			}
			// триллион
			if( val < 1E+12 )
			{
				val /= 1E+9;
				s = subStr(val.ToString() , 3);
				s = string.Concat(s , "B");
				return s;
			}

			// сто триллионов
			if( val < 1E+14 )
			{
				val /= 1E+12;
				s = subStr(val.ToString() , 4);
				s = string.Concat(s , "aa");
				return s;
			}
			// Квадриллион
			if( val < 1E+15 )
			{
				val /= 1E+12;
				s = subStr(val.ToString() , 3);
				s = string.Concat(s , "aa");
				return s;
			}
			return "";
		}

		private static string subStr( string str , int count )
		{
			if( str.Length <= count )
			{
				return str;
			}
			return str.Substring(0 , count);
		}
		public static string SecondsToString( this int val )
		{
			return SecondsToString((long)val);
		}

		public static string SecondsToString( this long val )
		{
			if( val <= 0 )
			{
				return "00:00";
			}
			string s = "";



			long seconds = val;

			long days = ( seconds / 86400 );
			seconds %= 86400;

			long hours = ( seconds / 3600 );
			seconds %= 3600;
			long minutes = ( seconds / 60 );
			long seconds_in_last_minute = seconds % 60;

			if( days != 0 )
			{
				s = string.Concat(s , days.ToString() , "d:");
			}

			if( hours != 0 )
			{
				addItem(hours , true);
			}
			addItem(minutes , true);
			addItem(seconds_in_last_minute , false);

			void addItem( long itemVal , bool b )
			{
				if( itemVal < 10 )
					s = string.Concat(s , '0' , itemVal.ToString());
				else
					s = string.Concat(s , itemVal.ToString());
				if( b )
				{
					s = string.Concat(s , ':');
				}
			}

			return s;
		}
		public static string SecondsToHoursMins( this int val )
		{
			if( val <= 0 )
			{
				return "0 m";
			}
			string s = "";



			int seconds = val;

			int hours = ( seconds / 3600 );
			seconds %= 3600;

			int minutes = ( seconds / 60 );

			if( hours > 0 )
			{
				s = string.Concat(s , hours.ToString() , "h");
			}
			if( minutes > 0 )
			{
				s = string.Concat(s , minutes.ToString() , "m");
			}

			return s;
		}

		public static string SecondsToStringDayHourMinSec( this int val )
		{
			if( val <= 0 )
			{
				return "00:00:00";
			}
			string s = "";



			int seconds = val;

			int days = ( seconds / 86400 );
			seconds %= 86400;

			int hours = ( seconds / 3600 );
			seconds %= 3600;
			int minutes = ( seconds / 60 );
			int seconds_in_last_minute = seconds % 60;

			if( days != 0 )
			{
				s = string.Concat(s , days.ToString() , "D.");
			}

			if( hours == 0 )
			{
				s = string.Concat(s , "00:");
			}
			else if( hours < 10 )
			{
				s = string.Concat(s , "0" , hours.ToString() , ":");
			}
			else
			{
				s = string.Concat(s , hours.ToString() , ":");
			}

			if( minutes == 0 )
			{
				s = string.Concat(s , "00:");
			}
			else if( minutes < 10 )
			{
				s = string.Concat(s , "0" , minutes.ToString() , ":");
			}
			else
			{
				s = string.Concat(s , minutes.ToString() , ":");
			}

			if( seconds_in_last_minute == 0 )
			{
				s = string.Concat(s , "00");
			}
			else if( seconds_in_last_minute < 10 )
			{
				s = string.Concat(s , "0" , seconds_in_last_minute.ToString());
			}
			else
			{
				s = string.Concat(s , seconds_in_last_minute.ToString());
			}

			return s;
		}

		public static float Normalize( this float val,float min, float max)
		{
			if( val < min )
				val = min;
			else if( val > max )
				val = max;
			return val;
		}

		public static string FloatToPercFromMultiplier( this float val )
		{
			float perc = Mathf.Round(( val - 1.0f ) * 100.0f);
			return string.Concat(perc.ToString() , "%");
		}
		public static string FloatToPercFromValue( this float val )
		{
			float perc = Mathf.Round(val * 100.0f);
			return string.Concat(perc.ToString() , "%");
		}
	}
}
