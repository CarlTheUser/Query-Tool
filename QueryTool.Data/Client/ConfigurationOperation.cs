using QueryTool.Data.Client.Data;
using QueryTool.Data.Model;
using QueryTool.Data.Provider;
using QueryTool.Data.Provider.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.Data.Client
{
    public class ConfigurationOperation
    {
        private readonly SqlConfigurationModelPersistence persistence = new XMLSqlConfigurationModelPersistence();

        public void Save(Data.SqlConfigurationData configurationData)
        {
            Model.SqlConfiguration configuration = new Model.SqlConfiguration()
            {
                Name = configurationData.Name,
                ConnectionString = configurationData.ConnectionString,
                ProviderType = configurationData.Provider.ToString()
            };

            configuration.Id = persistence.Save(configuration);

        }

        public void Edit(Data.SqlConfigurationData configurationData)
        {
            Model.SqlConfiguration configuration = new Model.SqlConfiguration()
            {
                Id = configurationData.Id,
                Name = configurationData.Name,
                ConnectionString = configurationData.ConnectionString,
                ProviderType = configurationData.Provider.ToString()
            };

            persistence.Edit(configuration.Id, configuration);
        }

        public void Delete(Data.SqlConfigurationData configurationData)
        {
            Model.SqlConfiguration configuration = new Model.SqlConfiguration()
            {
                Id = configurationData.Id,
                Name = configurationData.Name,
                ConnectionString = configurationData.ConnectionString,
                ProviderType = configurationData.Provider.ToString()
            };

            persistence.Delete(configuration.Id);
        }

        public IEnumerable<Data.SqlConfigurationData> GetAll()
        {
            var configurations = persistence.GetAll();
            return (from config in configurations
                    select new Data.SqlConfigurationData()
                    {
                        Id = config.Id,
                        Name = config.Name,
                        ConnectionString = config.ConnectionString,
                        Provider = GetProviderType(config.ProviderType)
                    }).ToList();
        }

        public SqlConfigurationData GetById(int id)
        {
            var configuration = persistence.GetById(id);
            return new Data.SqlConfigurationData()
            {
                Id = configuration.Id,
                Name = configuration.Name,
                ConnectionString = configuration.ConnectionString,
                Provider = GetProviderType(configuration.ProviderType)
            };
        }

        private ProviderType GetProviderType(string value)
        {
            ProviderType temp;
            Enum.TryParse(value, out temp);
            return temp;
        }
    }
}
