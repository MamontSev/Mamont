using System;

namespace Mamont.UI.MVVM
{
	public interface IPopUpView:IView
	{
		void Hide();
		void Show(Action<IPopUpView> OnCloze);
	}
}
