using Mamont.Data.DataControl.Resources;

namespace Mamont.EventsBus.Signals
{
	public class ResourceCountChangedSignal:IEventBusSignal
	{
		public readonly GameResourceType resType;
		public readonly float newCount;

		public ResourceCountChangedSignal( GameResourceType resType , float newCount )
		{
			this.resType = resType;
			this.newCount = newCount;
		}
	}
}



