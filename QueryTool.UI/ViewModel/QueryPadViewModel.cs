using QueryTool.UI.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QueryTool.UI.ViewModel
{
    class QueryPadViewModel : ViewModel
    {

        private string _query;

        public string Query
        {
            get => _query;
            set
            {
                _query = value;
                OnPropertyChanged("Query");
            }
        }

        public ICommand RunQueryCommand { get; private set; }

        public ICommand SaveQueryCommand { get; private set; }

        public QueryPadViewModel()
        {
            RunQueryCommand = new ParameterizedRelayCommand<string>(RunQuery);

            SaveQueryCommand = new ParameterizedRelayCommand<string>(SaveQuery);
        }

        private void RunQuery(string query)
        {
            if(query.Trim().Length > 0)
            {
                NotificationHub.GetInstance().ShowMessage($"Query: {query}.");
            }
            else NotificationHub.GetInstance().ShowMessage("Query is empty.");
        }

        private void SaveQuery(string query)
        {
            if (query.Trim().Length > 0)
            {
                NotificationHub.GetInstance().ShowMessage($"Query: {query}.");
            }
            else NotificationHub.GetInstance().ShowMessage("Query is empty.");
        }

    }
}
