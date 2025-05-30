namespace Mamont.Data.DataConfig.Text
{
	public interface ITextConfigData
	{
		string GetText( TextTypeGeneral type );
		string GetText( TextTypeGeneral type , params object[] args );

		string GetText( TextTypeMainMenuView type );
		string GetText( TextTypeMainMenuView type , params object[] args );
	}
}