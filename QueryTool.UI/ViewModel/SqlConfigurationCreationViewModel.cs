using QueryTool.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.UI.ViewModel
{
    public class SqlConfigurationCreationViewModel : ViewModel
    {
        public SqlConfiguration SqlConfiguration { get; set; }

        public SqlConfigurationCreationViewModel()
        {
            SqlConfiguration = SqlConfiguration.NewInstance();
        }
    }
}
