using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamont.UI.MVVM
{
	public interface IShowHideView:IView
	{
		void Hide();
		void Show();
		//void Bind<T>( T model ) where T : class, IViewModel; 				   
	}		 
}
							   