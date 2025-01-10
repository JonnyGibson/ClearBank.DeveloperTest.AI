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
            return _dataStoreType == "Backup" 
                ? new BackupAccountDataStore() 
                : new AccountDataStore();
        }
    }
} 