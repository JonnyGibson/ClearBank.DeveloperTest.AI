using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentSchemeValidator
    {
        bool ValidatePayment(Account account, MakePaymentRequest request);
    }
} 