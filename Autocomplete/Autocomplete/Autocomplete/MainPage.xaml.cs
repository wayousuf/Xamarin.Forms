using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Autocomplete
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            var items = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                items.Add(i);
            }

            listView.ItemsSource = items;           

        }        
    }
}
