using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.Data.Model
{
    class SqlConfiguration
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string ProviderType { get; set; }
    }
}
