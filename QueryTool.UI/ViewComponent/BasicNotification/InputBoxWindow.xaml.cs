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

namespace QueryTool.UI.ViewComponent.BasicNotification
{
    /// <summary>
    /// Interaction logic for InputBoxWindow.xaml
    /// </summary>
    public partial class InputBoxWindow : Window
    {
        public string UserInput => InputControl.Text;

        public InputBoxWindow(string message, string header = null)
        {
            InitializeComponent();
            Title.Text = header;
            Message.Text = message;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

    }
}
