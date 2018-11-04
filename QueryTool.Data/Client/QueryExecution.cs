using QueryTool.Data.Client.Data;
using QueryTool.Data.SQL;
using QueryTool.Data.SQL.Implementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.Data.Client
{
    public class QueryExecution
    {

        private readonly SqlCaller sqlCaller;

        public QueryExecution(SqlConfigurationData configuration)
        {
            switch (configuration.Provider)
            {
                case ProviderType.MySql:
                    sqlCaller = new SqlCaller(SqlProviderFactory.CreateProvider<MySqlProvider>(configuration.ConnectionString));
                    break;
                case ProviderType.OleDb:
                    sqlCaller = new SqlCaller(SqlProviderFactory.CreateProvider<OleDbProvider>(configuration.ConnectionString));
                    break;
                case ProviderType.SqlServer:
                    sqlCaller = new SqlCaller(SqlProviderFactory.CreateProvider<MsSqlProvider>(configuration.ConnectionString));
                    break;
            }
        }

        public QueryResult Execute(string query)
        {
            QueryResult result = null;

            string queryLow = query.ToLower();

            try
            {
                if (queryLow.Contains("select") || query.Contains("desc"))
                {
                    DataTable dt = sqlCaller.Query(query);
                    result = new QueryResult(dt);
                }
                else
                {
                    int recordsAffected = sqlCaller.ExecuteNonQuery(query);
                    result = new QueryResult(recordsAffected);
                }
            }
            catch
            {
                throw;
            }

            

            return result;
        }

        public class QueryResult
        {
            public int RecordsAffected { get; }

            public DataTable Table { get; }

            public QueryResult(DataTable table) => Table = table;

            public QueryResult(int recordsAffected) => RecordsAffected = recordsAffected;
            
        }
        
    }
}
