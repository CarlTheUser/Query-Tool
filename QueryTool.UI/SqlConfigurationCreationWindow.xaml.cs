using QueryTool.Data.Client.Data;
using QueryTool.UI.Model;
using QueryTool.UI.ViewComponent;
using QueryTool.UI.ViewModel;
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

namespace QueryTool.UI
{
    /// <summary>
    /// Interaction logic for SqlConfigurationCreationWindow.xaml
    /// </summary>
    public partial class SqlConfigurationCreationWindow : Window, IApplicationView
    {
        public SqlConfigurationCreationWindow()
        {
            InitializeComponent();
            ProviderSelectionInput.ItemsSource = Enum.GetValues(typeof(ProviderType)).Cast<ProviderType>();
        }

        public ViewModel.ViewModel GetViewModel()
        {
            return VM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VM.SqlConfiguration = null;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
