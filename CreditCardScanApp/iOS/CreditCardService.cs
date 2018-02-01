using System;
using System.Diagnostics;
using Card.IO;
using CreditCardScanApp.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(CreditCardService))]
namespace CreditCardScanApp.iOS
{
    public class CreditCardService : CardIOPaymentViewControllerDelegate, ICreditCardService
    {
        UIViewController _viewController;
        CreditCardInfo _creditCardInfo;

        public string CardholderName => _creditCardInfo?.CardholderName;
        public string CreditCardNumber => _creditCardInfo?.CardNumber;


        public void StartCapture()
        {
            InitCreditCardService();
            var cardIOPaymentViewController = new CardIOPaymentViewController(this);
            cardIOPaymentViewController.ScanExpiry = true;
            cardIOPaymentViewController.CollectCardholderName = true;
            _viewController.PresentViewController(cardIOPaymentViewController,true,null);
        }

        void InitCreditCardService()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            _viewController = window.RootViewController;
            while (_viewController.ParentViewController != null)
            {
                _viewController = _viewController.PresentedViewController;
            }
        }

        public override void UserDidCancelPaymentViewController(CardIOPaymentViewController paymentViewController)
        {
            Debug.WriteLine("Credit Card Cancel");
        }

        public override void UserDidProvideCreditCardInfo(CreditCardInfo cardInfo, CardIOPaymentViewController paymentViewController)
        {
            _creditCardInfo = cardInfo;

            Debug.WriteLine($"CardholderName : {cardInfo.CardholderName}");
            Debug.WriteLine($"Card Type : {cardInfo.CardType}");
            Debug.WriteLine($"ExpiryMonth : {cardInfo.ExpiryMonth}");
            Debug.WriteLine($"ExpiryYear : {cardInfo.ExpiryYear}");

            paymentViewController.DismissViewController(true, null);
        }
    }
}
