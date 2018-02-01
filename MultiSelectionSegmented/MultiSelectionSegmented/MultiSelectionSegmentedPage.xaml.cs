using System.Collections.Generic;
using Xamarin.Forms;

namespace MultiSelectionSegmented
{
    public partial class MultiSelectionSegmentedPage : ContentPage
    {
        public MultiSelectionSegmentedPage()
        {
            InitializeComponent();

            var tabs = new List<string> { "Tab 1", "Tab 2", "Tab 3", "Tab 4" };
            segmentControl.Items = tabs;

            segmentControl.SelectedItemsIndex = new List<int> { 0, 3 };

            segmentControl.SelectedItemAction += (obj) => {
                foreach (var item in obj)
                {
                    System.Diagnostics.Debug.WriteLine(item);
                }
            };  
        }
    }
}
