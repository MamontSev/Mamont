using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamont.UI.MVVM
{
	public interface IPopUpView:IView
	{
		void Hide();
		void Show(Action<IPopUpView> OnCloze);
	}
}
