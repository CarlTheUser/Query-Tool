using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QueryTool.UI.ViewModel;

namespace QueryTool.UI
{
    interface IApplicationView
    {
        ViewModel.ViewModel GetViewModel();
    }
}
