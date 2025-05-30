using Mamont.ServiceLoactor;

namespace Mamont.Log
{
	public interface ILogService
	{
		void Log( string message );
		void LogError( string message );
	}
}

