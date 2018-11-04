using QueryTool.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.UI.ViewLauncher
{
    class SqlConfigurationEditWindowLauncher : ISqlConfigurationEditViewLauncher
    {
        private SqlConfigurationEditWindow Window { get; set; }

        public void Launch(IDictionary<int, object> parameters)
        {
            Window = Window ?? new SqlConfigurationEditWindow();
            parameters.Add(new KeyValuePair<int, object>(SqlConfigurationEditViewModel.CLOSABLE_PARAMETER, Window));
            Window.GetViewModel().Parameters = parameters;
            Window.Topmost = true;
            Window.Show();
        }

        public void Close()
        {
            Window?.Close();
        }

    }
}
