using System;
namespace CreditCardScanApp
{
    public interface ICreditCardService
    {
        void StartCapture();
        string CreditCardNumber { get; }
        string CardholderName { get; }
    }
}
