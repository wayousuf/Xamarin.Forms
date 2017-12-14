using Movies.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Movies.MVVM
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            BindingContext = new MainViewModel();
			InitializeComponent();
		}
	}
}
