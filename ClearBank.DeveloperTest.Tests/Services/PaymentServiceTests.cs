using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentServiceTests
    {
        private readonly Mock<IAccountDataStore> _mockDataStore;
        private readonly Mock<IPaymentSchemeValidator> _mockValidator;
        private readonly PaymentService _service;
        private readonly Dictionary<PaymentScheme, IPaymentSchemeValidator> _validators;

        public PaymentServiceTests()
        {
            _mockDataStore = new Mock<IAccountDataStore>();
            _mockValidator = new Mock<IPaymentSchemeValidator>();
            _validators = new Dictionary<PaymentScheme, IPaymentSchemeValidator>
            {
                { PaymentScheme.Bacs, _mockValidator.Object }
            };
            _service = new PaymentService(_mockDataStore.Object, _validators);
        }

        [Fact]
        public void MakePayment_WhenValidatorNotFound_ReturnsFalse()
        {
            // Arrange
            var request = new MakePaymentRequest { PaymentScheme = PaymentScheme.Chaps };

            // Act
            var result = _service.MakePayment(request);

            // Assert
            Assert.False(result.Success);
            _mockDataStore.Verify(x => x.GetAccount(It.IsAny<string>()), Times.Once);
            _mockDataStore.Verify(x => x.UpdateAccount(It.IsAny<Account>()), Times.Never);
        }

        [Fact]
        public void MakePayment_WhenValidationFails_ReturnsFalse()
        {
            // Arrange
            var account = new Account();
            var request = new MakePaymentRequest { PaymentScheme = PaymentScheme.Bacs };
            _mockDataStore.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);
            _mockValidator.Setup(x => x.ValidatePayment(account, request)).Returns(false);

            // Act
            var result = _service.MakePayment(request);

            // Assert
            Assert.False(result.Success);
            _mockDataStore.Verify(x => x.UpdateAccount(It.IsAny<Account>()), Times.Never);
        }

        [Fact]
        public void MakePayment_WhenValidationPasses_UpdatesAccountAndReturnsFalse()
        {
            // Arrange
            var account = new Account { Balance = 100 };
            var request = new MakePaymentRequest 
            { 
                PaymentScheme = PaymentScheme.Bacs,
                Amount = 50
            };
            _mockDataStore.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);
            _mockValidator.Setup(x => x.ValidatePayment(account, request)).Returns(true);

            // Act
            var result = _service.MakePayment(request);

            // Assert
            Assert.False(result.Success); // Known bug: Success is always false
            Assert.Equal(50, account.Balance);
            _mockDataStore.Verify(x => x.UpdateAccount(account), Times.Once);
        }
    }
} 