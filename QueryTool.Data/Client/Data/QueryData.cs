using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.Data.Client.Data
{
    public class QueryData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public ProviderType Provider { get; set; }
    }
}
