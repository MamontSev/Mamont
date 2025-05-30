namespace Mamont.EventsBus.Signals
{
	public class HouseDoorBrokenSignal:IEventBusSignal
	{
		public readonly string HouseName;
		public HouseDoorBrokenSignal( string HouseName )
		{
			this.HouseName = HouseName;
		}
	}
}



