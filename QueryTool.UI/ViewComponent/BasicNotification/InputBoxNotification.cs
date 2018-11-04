using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.UI.ViewComponent.BasicNotification
{
    class InputBoxNotification : INotification<string>
    {
        public string Notify(string heading, string message)
        {
            string returnString = string.Empty;

            InputBoxWindow inputBoxWindow = new InputBoxWindow(message, heading);

            inputBoxWindow.ShowDialog();

            returnString = inputBoxWindow.UserInput;

            return returnString;
        }

        public string Notify(string message) { return Notify(null, message); }
    }
}
