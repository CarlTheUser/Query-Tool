using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using QueryTool.UI.ViewModel;

namespace QueryTool.UI
{
    /// <summary>
    /// Interaction logic for SqlConfigurationEditWindow.xaml
    /// </summary>
    public partial class SqlConfigurationEditWindow : Window, IApplicationView
    {
        public SqlConfigurationEditWindow()
        {
            InitializeComponent();
        }

        public ViewModel.ViewModel GetViewModel()
        {
            throw new NotImplementedException();
        }
    }
}
