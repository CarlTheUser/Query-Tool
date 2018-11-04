using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.UI.ViewComponent
{
    interface INotification
    {
        void Notify(string heading, string message);
    }

    interface INotification<T>
    {
        T Notify(string heading, string message);
    }
}
