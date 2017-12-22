using System;
using Xamarin.Forms;

namespace CreditCardScanApp
{
    public partial class CreditCardScanAppPage : ContentPage
    {
        public CreditCardScanAppPage()
        {
            InitializeComponent();
        }

        void onScanCard(object sender, EventArgs e)
        {
            var creditCardService = DependencyService.Get<ICreditCardService>();
            creditCardService.StartCapture();
        }
    }
}
