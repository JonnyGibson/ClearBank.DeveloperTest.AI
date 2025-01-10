using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using System.Collections.Generic;

namespace ClearBank.DeveloperTest.Services
{
    /// <summary>
    /// Service for processing payments using different payment schemes.
    /// Note: Known bug - payments always return false even when successful.
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly IDictionary<PaymentScheme, IPaymentSchemeValidator> _validators;

        public PaymentService(IAccountDataStore accountDataStore, IDictionary<PaymentScheme, IPaymentSchemeValidator> validators)
        {
            _accountDataStore = accountDataStore;
            _validators = validators;
        }

        /// <summary>
        /// Processes a payment request.
        /// Known bug: Success is always false, even for valid payments.
        /// The balance is still updated if validation passes.
        /// </summary>
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountDataStore.GetAccount(request.DebtorAccountNumber);
            // Success defaults to false and is never set to true (known bug)
            var result = new MakePaymentResult();

            if (_validators.TryGetValue(request.PaymentScheme, out var validator))
            {
                if (!validator.ValidatePayment(account, request))
                {
                    result.Success = false;
                }
                else
                {
                    // Even though validation passed, Success remains false
                    account.Balance -= request.Amount;
                    _accountDataStore.UpdateAccount(account);
                }
            }

            return result;
        }
    }
}
