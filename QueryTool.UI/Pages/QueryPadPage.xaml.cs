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
using System.Windows.Navigation;
using System.Windows.Shapes;
using QueryTool.UI.ViewModel;

namespace QueryTool.UI.Pages
{
    /// <summary>
    /// Interaction logic for QueryPadPage.xaml
    /// </summary>
    public partial class QueryPadPage : BasePage
    {
        public QueryPadPage()
        {
            InitializeComponent();
        }

        public override ViewModel.ViewModel GetViewModel()
        {
            throw new NotImplementedException();
        }
    }
}
