using ClearBank.DeveloperTest.Data;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Data
{
    public class AccountDataStoreFactoryTests
    {
        [Fact]
        public void Create_WhenDataStoreTypeIsBackup_ReturnsBackupAccountDataStore()
        {
            // Arrange
            var factory = new AccountDataStoreFactory("Backup");

            // Act
            var result = factory.Create();

            // Assert
            Assert.IsType<BackupAccountDataStore>(result);
        }

        [Theory]
        [InlineData("Live")]
        [InlineData("")]
        [InlineData(null)]
        public void Create_WhenDataStoreTypeIsNotBackup_ReturnsAccountDataStore(string dataStoreType)
        {
            // Arrange
            var factory = new AccountDataStoreFactory(dataStoreType);

            // Act
            var result = factory.Create();

            // Assert
            Assert.IsType<AccountDataStore>(result);
        }
    }
} 