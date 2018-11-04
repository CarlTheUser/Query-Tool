using QueryTool.UI.Command;
using QueryTool.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QueryTool.UI.ViewModel
{
    class QueryListItemViewModel : ViewModel
    {
        public ICommand RunCommand { get; private set; }

        public ICommand CopyCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand SaveEditCommand { get; private set; }

        public ICommand CancelEditCommand { get; private set; }

        public Query Query { get; set; }

        public QueryListItemViewModel()
        {
            RunCommand = new RelayCommand(RunQuery);
            CopyCommand = new RelayCommand(CopyQuery);
            EditCommand = new RelayCommand(Edit);
            SaveEditCommand = new RelayCommand(SaveEdit);
            CancelEditCommand = new RelayCommand(CancelEdit);
        }

        private void RunQuery()
        {

            if (QueryConfiguration.GetInstance().CurrentConfiguration.Provider == Query.Provider)
            {
                Dictionary<int, object> parameters = new Dictionary<int, object>()
                {
                    { QueryPadViewModel.INITIALQUERY_PARAMETER, Query.Value }
                };
                App.Current.MainView.UserNavigation.Navigate(new Navigation.NavigationItem(Pages.ApplicationPage.QueryPad, true, parameters));
            }
            else NotificationHub.GetInstance().ShowMessage("Current configuration doesn't match query provider. Change current configuration.");
        }

        private void CopyQuery()
        {
            Clipboard.SetText(Query.Value);
            NotificationHub.GetInstance().ShowMessage("Query copied to clipboard");
        }

        private void Edit()
        {
            Query.BeginEdit();
        }

        private void SaveEdit()
        {
            Query.ApplyEdit();
        }

        private void CancelEdit()
        {
            Query.CancelEdit();
        }
    }
}
