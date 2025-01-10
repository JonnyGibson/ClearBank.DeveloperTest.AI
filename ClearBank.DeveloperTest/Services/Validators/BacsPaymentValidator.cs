using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Validators
{
    public class BacsPaymentValidator : IPaymentSchemeValidator
    {
        public bool ValidatePayment(Account account, MakePaymentRequest request)
        {
            return account != null && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);
        }
    }
} 