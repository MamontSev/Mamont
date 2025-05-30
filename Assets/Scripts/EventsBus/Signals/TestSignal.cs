namespace Mamont.EventsBus.Signals
{
	public class TestSignal:IEventBusSignal
	{
		public readonly int Value;

		public TestSignal( int value )
		{
			this.Value = value;
		}
	}
}



