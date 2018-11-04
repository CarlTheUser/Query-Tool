using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using QueryTool.UI.ViewModel;

namespace QueryTool.UI.Pages
{
    public abstract class BasePage : Page, IApplicationView
    {

        public static readonly IDictionary<ApplicationPage, Type> ApplicationPageRegistry;

        static BasePage()
        {
            ApplicationPageRegistry = new Dictionary<ApplicationPage, Type>()
            {
                { ApplicationPage.QueryPad, typeof(QueryPadPage) },
                { ApplicationPage.Configuration, typeof(ConfigurationPage) },
                { ApplicationPage.SavedQueries, typeof(SavedQueriesPage) }
            };
        }

        public abstract ViewModel.ViewModel GetViewModel();

    }
}
