using QueryTool.Data.Client;
using QueryTool.Data.Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.UI.Model
{
    public sealed class SqlConfiguration : PersistibleModel
    {

        private int _id;
        private string _name;
        private string _connectionString;
        private ProviderType _provider;

        #region Public Properties

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                OnPropertyChanged("ConnectionString");
            }
        }

        public ProviderType Provider
        {
            get => _provider;
            set
            {
                _provider = value;
                OnPropertyChanged("Provider");
            }
        }

        #endregion

        #region Factory Methods

        public static SqlConfiguration NewInstance() { return new SqlConfiguration() { State = ModelState.New }; }

        public static SqlConfiguration Existing(int id, string name, string connectionString, ProviderType provider) { return new SqlConfiguration(id, name, connectionString, provider) { State = ModelState.Old }; }

        #endregion

        #region Constructors

        private SqlConfiguration() { }

        private SqlConfiguration(int id, string name, string connectionString, ProviderType provider)
        {
            Id = id;
            Name = name;
            ConnectionString = connectionString;
            Provider = provider;
        }

        #endregion

        #region Protected Implementations

        private string name_backup = null;
        private string connectionString_backup = null;
        private ProviderType? provider_backup = null;

        protected override void BackupProperties()
        {
            name_backup = _name;
            connectionString_backup = _connectionString;
            provider_backup = _provider;
        }

        protected override void RestoreProperties()
        {
            Name = name_backup;
            ConnectionString = connectionString_backup;
            if(provider_backup.HasValue) Provider = provider_backup.Value;
        }

        protected override void ClearPropertyBackups()
        {
            name_backup = connectionString_backup = null;
            provider_backup = null;
        }

        protected override void SaveMethod()
        {
            try
            {
                new ConfigurationOperation().Save(ToDTOForm());
            }
            catch (Exception ex)
            {
                OnErrorOccured(ex.Message);
            }
        }
        
        protected override void EditMethod()
        {
            try
            {
                new ConfigurationOperation().Edit(ToDTOForm());
            }
            catch (Exception ex)
            {
                OnErrorOccured(ex.Message);
            }
        }

        protected override void DeleteMethod()
        {
            try
            {
                new ConfigurationOperation().Delete(ToDTOForm());
            }
            catch (Exception ex)
            {
                OnErrorOccured(ex.Message);
            }
        }

        #endregion

        private SqlConfigurationData ToDTOForm()
        {
            return new SqlConfigurationData()
            {
                Id = Id,
                Name = Name,
                ConnectionString = ConnectionString,
                Provider = Provider
            };
        }

    }
}
