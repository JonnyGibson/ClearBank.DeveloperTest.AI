namespace ClearBank.DeveloperTest.Data
{
    public class AccountDataStoreFactory
    {
        private readonly string _dataStoreType;

        public AccountDataStoreFactory(string dataStoreType)
        {
            _dataStoreType = dataStoreType;
        }

        public IAccountDataStore Create()
        {
            if (_dataStoreType == "Backup")
            {
                return new BackupAccountDataStore();
            }
            return new AccountDataStore();
        }
    }
} 