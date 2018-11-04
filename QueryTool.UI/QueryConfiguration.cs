using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryTool.UI.Model;

namespace QueryTool.UI
{
    public sealed class QueryConfiguration
    {
        private static readonly QueryConfiguration INSTANCE = new QueryConfiguration();

        public static QueryConfiguration GetInstance() => INSTANCE;

        private QueryConfiguration() { }
        
        public SqlConfiguration CurrentConfiguration { get; set; }


    }
}
