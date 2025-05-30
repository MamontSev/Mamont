using System;

namespace Mamont.Data.DataControl.TimeSpanControl
{
	public interface ITimeSpanService
	{
		void Start( TimeSpanType type );
		void Stop( TimeSpanType type );
		void CheckState( TimeSpanType type , Action onNotStarted = null, Action<long> onContinue = null , Action onComplete = null );
	}
}
