using QueryTool.Data.Client;
using QueryTool.Data.Client.Data;
using QueryTool.UI.Command;
using QueryTool.UI.Model;
using QueryTool.UI.ViewComponent;
using QueryTool.UI.ViewLauncher;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QueryTool.UI.ViewModel
{
    public class ConfigurationViewModel : ViewModel
    {
        public ICommand NewSqlConfigurationCommand { get; private set; }

        public ICommand UseSqlConfigurationCommand { get; private set; }

        public ICommand EditSqlConfigurationCommand { get; private set; }

        public ObservableCollection<SqlConfiguration> Configurations { get; set; }

        private readonly ConfigurationOperation persistence = new ConfigurationOperation();

        private SqlConfiguration _currentConfiguration;

        public SqlConfiguration CurrentConfiguration

        {
            get => _currentConfiguration;
            private set
            {
                _currentConfiguration = value;
                OnPropertyChanged("CurrentConfiguration");
                OnPropertyChanged("HasConfiguration");
            }
        }

        public bool HasConfiguration => CurrentConfiguration != null;

        public ConfigurationViewModel()
        {
            var temp = persistence.GetAll().ToList();

            List<SqlConfiguration> configurationsList = (from configuration in temp
                                                         select SqlConfiguration.Existing(
                                                             configuration.Id,
                                                             configuration.Name,
                                                             configuration.ConnectionString,
                                                             configuration.Provider)).ToList();


            Configurations = new ObservableCollection<SqlConfiguration>(configurationsList);



            NewSqlConfigurationCommand = new RelayCommand(NewSqlConfiguration);

            UseSqlConfigurationCommand = new ParameterizedRelayCommand<SqlConfiguration>(UseSqlConfiguration);

        }

        private void NewSqlConfiguration()
        {
            IInputComponent<SqlConfiguration> input = new SqlConfigurationWindowInput();

            SqlConfiguration configuration = input.GetInput();

            if(configuration != null)
            {
                configuration.Saved += Configuration_Saved;
                configuration.ErrorOccured += Configuration_ErrorOccured;

                configuration.Save();
            }
        }

        private void UseSqlConfiguration(SqlConfiguration configuration)
        {
            QueryConfiguration.GetInstance().CurrentConfiguration = CurrentConfiguration = configuration;
        }

        private void EditSqlConfiguration(SqlConfiguration configuration)
        {
            ISqlConfigurationEditViewLauncher viewLauncher = new SqlConfigurationEditWindowLauncher();

            IDictionary<int, object> parameters = new Dictionary<int, object>()
            {
                { SqlConfigurationEditViewModel.SQLCONFIGURATION_MODEL_PARAMETER, configuration }
            };

            viewLauncher.Launch(parameters);
        }

        private void Configuration_ErrorOccured(object sender, PersistibleModel.ErrorOccuredEventArgs e)
        {
            SqlConfiguration temp = (SqlConfiguration)sender;
            temp.Saved -= Configuration_Saved;
            temp.ErrorOccured -= Configuration_ErrorOccured;
            NotificationHub.GetInstance().ShowMessage(e.Message);
        }

        private void Configuration_Saved(object sender, EventArgs e)
        {
            SqlConfiguration saved = (SqlConfiguration)sender;
            Configurations.Add(saved);
            NotificationHub.GetInstance().ShowMessage($"Sql configuration for {saved.Name} created successfully.");
            saved.Saved -= Configuration_Saved;
            saved.ErrorOccured -= Configuration_ErrorOccured;
        }
    }

}
