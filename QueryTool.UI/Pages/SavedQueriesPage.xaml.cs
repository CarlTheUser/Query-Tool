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
    /// Interaction logic for SavedQueriesPage.xaml
    /// </summary>
    public partial class SavedQueriesPage : BasePage
    {
        public SavedQueriesPage()
        {
            InitializeComponent();
        }

        public override ViewModel.ViewModel GetViewModel()
        {
            return VM;
        }
    }
}
