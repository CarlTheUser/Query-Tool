
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.Data.SQL
{
    class SqlProviderFactory
    {
        static ISqlProvider CreateProvider<TProvider>(string connectionString) where TProvider : ISqlProvider, new()
        {
            TProvider instance = new TProvider
            {
                ConnectionString = connectionString
            };
            return instance;

        }
    }
}
