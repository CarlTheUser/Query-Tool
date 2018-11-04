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
using QueryTool.Data.Client.Data;
using QueryTool.UI.ViewModel;

namespace QueryTool.UI.Pages
{
    /// <summary>
    /// Interaction logic for ConfigurationPage.xaml
    /// </summary>
    public partial class ConfigurationPage : BasePage
    {
        public ConfigurationPage()
        {
            InitializeComponent();
            //ConfigurationProviderInput.ItemsSource = Enum.GetValues(typeof(ProviderType)).Cast<ProviderType>();
        }

        public override ViewModel.ViewModel GetViewModel()
        {
            return VM;
        }
    }
}
