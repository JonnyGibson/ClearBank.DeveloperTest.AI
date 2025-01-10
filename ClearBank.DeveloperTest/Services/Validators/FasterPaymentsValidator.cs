using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Validators
{
    public class FasterPaymentsValidator : IPaymentSchemeValidator
    {
        public bool ValidatePayment(Account account, MakePaymentRequest request)
        {
            return account != null && 
                   account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) &&
                   account.Balance >= request.Amount;
        }
    }
} 