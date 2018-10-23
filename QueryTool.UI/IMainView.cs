using QueryTool.UI.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.UI
{
    public interface IMainView
    {
        IUserNavigation UserNavigation { get; }
    }
}
