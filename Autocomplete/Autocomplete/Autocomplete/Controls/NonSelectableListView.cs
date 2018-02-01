using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Autocomplete.Controls
{
    public class NonSelectableListView : ListView
    {
        public NonSelectableListView() : base (ListViewCachingStrategy.RecycleElement)
        {
            
        }
    }
}
