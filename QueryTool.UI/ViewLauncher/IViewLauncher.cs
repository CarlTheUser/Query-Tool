using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.UI.ViewLauncher
{
    interface IViewLauncher
    {
        void Launch(IDictionary<int, object> parameters);
    }
}
