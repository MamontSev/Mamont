﻿using UnityEngine;

namespace Mamont.Log
{
	public class LogService:  ILogService
	{
		public void Log( string message  )
		{
			Debug.Log(message);
		}

		public void LogError( string message )
		{
			Debug.LogError(message);
		}
	}
}
