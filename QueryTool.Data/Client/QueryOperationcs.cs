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
    public class QueryOperationcs
    {
        private readonly QueryModelPersistence persistence = new XMLQueryModelPersistence();

        public void Save(QueryData queryData)
        {
            Query query = new Query()
            {
                Name = queryData.Name,
                Value = queryData.Value,
                Provider = queryData.Provider.ToString()
            };

            query.Id = persistence.Save(query);

        }

        public void Edit(QueryData queryData)
        {
            Query query = new Query()
            {
                Id = queryData.Id,
                Name = queryData.Name,
                Value = queryData.Value,
                Provider = queryData.Provider.ToString()
            };

            persistence.Edit(query.Id, query);
        }

        public void Delete(QueryData queryData)
        {
            Query query = new Query()
            {
                Id = queryData.Id,
                Name = queryData.Name,
                Value = queryData.Value,
                Provider = queryData.Provider.ToString()
            };

            persistence.Delete(query.Id);
        }

        public IEnumerable<QueryData> GetAll()
        {
            var configurations = persistence.GetAll();
            return (from config in configurations
                    select new QueryData()
                    {
                        Id = config.Id,
                        Name = config.Name,
                        Value = config.Value,
                        Provider = GetProviderType(config.Provider)
                    }).ToList();
        }

        public QueryData GetById(int id)
        {
            var query = persistence.GetById(id);
            return new QueryData()
            {
                Id = query.Id,
                Name = query.Name,
                Value = query.Value,
                Provider = GetProviderType(query.Provider)
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
