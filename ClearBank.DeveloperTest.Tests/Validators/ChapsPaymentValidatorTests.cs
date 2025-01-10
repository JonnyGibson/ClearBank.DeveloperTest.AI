using ClearBank.DeveloperTest.Services.Validators;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class ChapsPaymentValidatorTests
    {
        private readonly ChapsPaymentValidator _validator;

        public ChapsPaymentValidatorTests()
        {
            _validator = new ChapsPaymentValidator();
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
        public void ValidatePayment_WhenAccountDoesNotAllowChaps_ReturnsFalse()
        {
            // Arrange
            var account = new Account 
            { 
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Status = AccountStatus.Live
            };
            var request = new MakePaymentRequest();

            // Act
            var result = _validator.ValidatePayment(account, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidatePayment_WhenAccountIsNotLive_ReturnsFalse()
        {
            // Arrange
            var account = new Account 
            { 
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Disabled
            };
            var request = new MakePaymentRequest();

            // Act
            var result = _validator.ValidatePayment(account, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidatePayment_WhenAccountAllowsChapsAndIsLive_ReturnsTrue()
        {
            // Arrange
            var account = new Account 
            { 
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Live
            };
            var request = new MakePaymentRequest();

            // Act
            var result = _validator.ValidatePayment(account, request);

            // Assert
            Assert.True(result);
        }
    }
} 