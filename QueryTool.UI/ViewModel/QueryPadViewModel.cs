using QueryTool.Data.Client;
using QueryTool.Data.Client.Data;
using QueryTool.UI.Command;
using QueryTool.UI.Model;
using QueryTool.UI.ViewComponent;
using QueryTool.UI.ViewComponent.BasicNotification;
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
        public static readonly int INITIALQUERY_PARAMETER = 1;

        private readonly QueryExecution queryExecution;

        private string _currentQuery;

        public string CurrentQuery
        {
            get => _currentQuery;
            set
            {
                _currentQuery = value;
                OnPropertyChanged("CurrentQuery");
            }
        }

        public ICommand RunQueryCommand { get; private set; }

        public ICommand SaveQueryCommand { get; private set; }

        private object _resultSet;

        public object ResultSet
        {
            get => _resultSet;
            set
            {
                _resultSet = value;
                OnPropertyChanged("ResultSet");
            }
        }

        #region Constructor

        public QueryPadViewModel()
        {
            RunQueryCommand = new ParameterizedRelayCommand<string>(RunQuery);

            SaveQueryCommand = new ParameterizedRelayCommand<string>(SaveQuery);

            SqlConfiguration configuration = QueryConfiguration.GetInstance().CurrentConfiguration;

            if (configuration == null)
            {
                configuration = SqlConfiguration.Existing(1, "Temp", "server=localhost;database=dbbernasorrags;user=root;password=;", ProviderType.MySql);


                QueryConfiguration.GetInstance().CurrentConfiguration = configuration;
            }
            
            queryExecution = new QueryExecution(new SqlConfigurationData()
            {
                Id = configuration.Id,
                Name = configuration.Name,
                ConnectionString = configuration.ConnectionString,
                Provider = configuration.Provider
            });

        }

        #endregion

        private void RunQuery(string query)
        {
            if(query.Trim().Length > 0)
            {
                //ISqlProvider provider = SqlProviderFactory.CreateProvider<OleDbProvider>("Provider=IBMDA400.DataSource; Data Source=199.84.0.105; Password=JBANTO; User ID=JBANTO; Default Collection=MMJDALIB");
                //SqlCaller caller = new SqlCaller(provider);
                //ResultSet = caller.Query(query).DefaultView;
                try
                {
                    QueryExecution.QueryResult result = queryExecution.Execute(query);

                    if (result.Table != null) ResultSet = result.Table.DefaultView;
                    else NotificationHub.GetInstance().ShowMessage($"{result.RecordsAffected} Records Affected.");
                }
                catch (Exception e)
                {
                    NotificationHub.GetInstance().ShowMessage(e.Message);
                }

            }
            else NotificationHub.GetInstance().ShowMessage("Query is empty.");
        }

        private void SaveQuery(string query)
        {
            if (query.Trim().Length > 0)
            {
                INotification<string> notification = new InputBoxNotification();

                string name = notification.Notify("Query Name", "Enter a name for the query.");

                if ((name = name.Trim()).Length > 0)
                {
                    Query newQuery = Query.NewInstance();
                    newQuery.Name = name;
                    newQuery.Provider = QueryConfiguration.GetInstance().CurrentConfiguration.Provider;
                    newQuery.Value = query;

                    newQuery.Saved += NewQuery_Saved;
                    newQuery.Save();
                }
            }
            else NotificationHub.GetInstance().ShowMessage("Query is empty.");
        }

        private void NewQuery_Saved(object sender, EventArgs e)
        {
            NotificationHub.GetInstance().ShowMessage("Query saved.");
            ((Query)sender).Saved -= NewQuery_Saved;
        }

        protected override void OnParameterSet(IDictionary<int, object> parameters)
        {
            if(parameters.ContainsKey(INITIALQUERY_PARAMETER))
            {
                string query = (string)parameters[INITIALQUERY_PARAMETER];

                if (query != null && query.Trim().Length > 0) RunQuery(CurrentQuery = query);
            }
            base.OnParameterSet(parameters);
        }

    }
}
