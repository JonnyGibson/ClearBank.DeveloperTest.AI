using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Validators
{
    public class FasterPaymentsValidator : IPaymentSchemeValidator
    {
        /// <summary>
        /// Validates if a payment can be processed through the Faster Payments scheme.
        /// </summary>
        /// <param name="account">The account from which the payment will be made.</param>
        /// <param name="request">The payment request details.</param>
        /// <returns>
        /// True if all the following conditions are met:
        /// - Account exists (not null)
        /// - Account is authorized for Faster Payments
        /// - Account has sufficient balance to cover the payment amount
        /// </returns>
        public bool ValidatePayment(Account account, MakePaymentRequest request)
        {
            return account != null && 
                   account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) &&
                   account.Balance >= request.Amount;
        }
    }
} 