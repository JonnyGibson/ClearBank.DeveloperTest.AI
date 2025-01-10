using ClearBank.DeveloperTest.Services.Validators;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class FasterPaymentsValidatorTests
    {
        private readonly FasterPaymentsValidator _validator;

        public FasterPaymentsValidatorTests()
        {
            _validator = new FasterPaymentsValidator();
        }

        [Fact]
        public void ValidatePayment_WhenAccountIsNull_ReturnsFalse()
        {
            // Arrange
            Account account = null;
            var request = new MakePaymentRequest { Amount = 100 };

            // Act
            var result = _validator.ValidatePayment(account, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidatePayment_WhenAccountDoesNotAllowFasterPayments_ReturnsFalse()
        {
            // Arrange
            var account = new Account 
            { 
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Balance = 100
            };
            var request = new MakePaymentRequest { Amount = 50 };

            // Act
            var result = _validator.ValidatePayment(account, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidatePayment_WhenBalanceIsInsufficient_ReturnsFalse()
        {
            // Arrange
            var account = new Account 
            { 
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 50
            };
            var request = new MakePaymentRequest { Amount = 100 };

            // Act
            var result = _validator.ValidatePayment(account, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidatePayment_WhenAccountAllowsFasterPaymentsAndHasSufficientBalance_ReturnsTrue()
        {
            // Arrange
            var account = new Account 
            { 
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 100
            };
            var request = new MakePaymentRequest { Amount = 50 };

            // Act
            var result = _validator.ValidatePayment(account, request);

            // Assert
            Assert.True(result);
        }
    }
} 