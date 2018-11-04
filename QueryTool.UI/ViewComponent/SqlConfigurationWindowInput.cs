using QueryTool.UI.Model;
using QueryTool.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.UI.ViewComponent
{
    class SqlConfigurationWindowInput : IInputComponent<SqlConfiguration>
    {
        public SqlConfiguration GetInput()
        {
            SqlConfigurationCreationWindow window = new SqlConfigurationCreationWindow();
            IApplicationView view = window;
            window.ShowDialog();
            return ((SqlConfigurationCreationViewModel)view.GetViewModel()).SqlConfiguration;
        }
    }
}
