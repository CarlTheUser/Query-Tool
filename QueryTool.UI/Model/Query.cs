using QueryTool.Data.Client;
using QueryTool.Data.Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.UI.Model
{
    public sealed class Query : PersistibleModel
    {
        private int _id;
        private string _name;
        private string _value;
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

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged("Value");
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

        public static Query NewInstance()
        {
            return new Query
            {
                State = ModelState.New
            };
        }

        public static Query Existing(int id, string name, string value, ProviderType provider)
        {
            return new Query(id, name, value, provider)
            {
                State = ModelState.Old
            };
        }

        #endregion

        #region Constructors

        private Query(int id, string name, string value, ProviderType provider)
        {
            Id = id;
            Name = name;
            Value = value;
            Provider = provider;
        }

        private Query()
        {

        }

        #endregion

        private string name_backup = null;
        private string value_backup = null;
        private ProviderType? provider_backup = null;

        protected override void BackupProperties()
        {
            name_backup = _name;
            value_backup = _value;
            provider_backup = _provider;
        }

        protected override void ClearPropertyBackups()
        {
            name_backup = value_backup = null;
            provider_backup = null;
        }

        protected override void RestoreProperties()
        {
            Name = name_backup;
            Value = value_backup;
            if(provider_backup.HasValue) Provider = provider_backup.Value;
        }

        protected override void SaveMethod()
        {
            try
            {
                QueryData query = ToDTOForm();
                new QueryOperationcs().Save(query);
                Id = query.Id;
            }
            catch(Exception ex)
            {
                OnErrorOccured(ex.Message);
            }
        }

        protected override void EditMethod()
        {
            try
            {
                new QueryOperationcs().Edit(ToDTOForm());
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
                new QueryOperationcs().Delete(ToDTOForm());
            }
            catch (Exception ex)
            {
                OnErrorOccured(ex.Message);
            }
        }

        private QueryData ToDTOForm()
        {
            return new QueryData
            {
                Id = Id,
                Name = Name,
                Value = Value,
                Provider = Provider
            };
        }

    }
}
