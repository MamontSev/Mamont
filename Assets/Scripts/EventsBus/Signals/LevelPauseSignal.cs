namespace Mamont.EventsBus.Signals
{
	public class LevelPauseSignal:IEventBusSignal
	{
		public readonly bool IsPuase;
		public LevelPauseSignal( bool IsPuase )
		{
			this.IsPuase = IsPuase;
		}
	}
}



