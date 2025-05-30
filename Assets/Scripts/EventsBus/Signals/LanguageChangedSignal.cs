using Mamont.Data.DataControl.Language;

namespace Mamont.EventsBus.Signals
{
	public class LanguageChangedSignal:IEventBusSignal
	{
		public readonly LanguageType newLanguage;

		public LanguageChangedSignal( LanguageType newLanguage )
		{
			this.newLanguage = newLanguage;
		}
	}
}



