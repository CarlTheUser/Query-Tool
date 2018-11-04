using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.Data.Client.Data
{
    public class SqlConfigurationData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public ProviderType Provider { get; set; }
    }
}
