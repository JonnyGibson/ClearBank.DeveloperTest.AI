using ClearBank.DeveloperTest.Services.Validators;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class BacsPaymentValidatorTests
    {
        private readonly BacsPaymentValidator _validator;

        public BacsPaymentValidatorTests()
        {
            _validator = new BacsPaymentValidator();
        }

        [Fact]
        public void ValidatePayment_WhenAccountIsNull_ReturnsFalse()
        {
            // Arrange
            Account account = null;
            var request = new MakePaymentRequest();

            // Act
            var result = _validator.ValidatePayment(account, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidatePayment_WhenAccountDoesNotAllowBacs_ReturnsFalse()
        {
            // Arrange
            var account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments };
            var request = new MakePaymentRequest();

            // Act
            var result = _validator.ValidatePayment(account, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidatePayment_WhenAccountAllowsBacs_ReturnsTrue()
        {
            // Arrange
            var account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs };
            var request = new MakePaymentRequest();

            // Act
            var result = _validator.ValidatePayment(account, request);

            // Assert
            Assert.True(result);
        }
    }
} 