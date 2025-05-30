using Mamont.UI.MVVM;

namespace Mamont.UI.General.PopUp
{
	public interface IBasePopUpFactory
	{
		void Show<T>( T vm ) where T : IPopUpViewModel;
	}
}
