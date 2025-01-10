using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Validators
{
    public class ChapsPaymentValidator : IPaymentSchemeValidator
    {
        /// <summary>
        /// Validates if a payment can be processed through the CHAPS scheme.
        /// </summary>
        /// <param name="account">The account from which the payment will be made.</param>
        /// <param name="request">The payment request details.</param>
        /// <returns>
        /// True if all the following conditions are met:
        /// - Account exists (not null)
        /// - Account is authorized for CHAPS payments
        /// - Account status is Live
        /// </returns>
        public bool ValidatePayment(Account account, MakePaymentRequest request)
        {
            return account != null && 
                   account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) &&
                   account.Status == AccountStatus.Live;
        }
    }
} 