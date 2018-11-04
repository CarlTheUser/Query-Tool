using QueryTool.UI.Command;
using QueryTool.UI.Model;
using QueryTool.UI.ViewComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QueryTool.UI.ViewModel
{
    class SqlConfigurationEditViewModel : ViewModel
    {
        public static readonly int SQLCONFIGURATION_MODEL_PARAMETER = 1;

        public static readonly int CLOSABLE_PARAMETER = 2;

        public SqlConfiguration SqlConfiguration { get; set; }

        public ICommand SaveChangesCommand { get; private set; }

        public ICommand CancelEditCommand { get; private set; }

        private IClosable Closable;

        public SqlConfigurationEditViewModel()
        {
            SaveChangesCommand = new RelayCommand(ApplyEdit);
            CancelEditCommand = new RelayCommand(CancelEdit);
        }

        protected override void OnParameterSet(IDictionary<int, object> parameters)
        {
            base.OnParameterSet(parameters);
            if(parameters != null)
            {
                if (parameters.ContainsKey(SQLCONFIGURATION_MODEL_PARAMETER) && parameters.ContainsKey(CLOSABLE_PARAMETER))
                {
                    Closable = (IClosable)parameters[CLOSABLE_PARAMETER];
                    SqlConfiguration = (SqlConfiguration)parameters[SQLCONFIGURATION_MODEL_PARAMETER];
                    SqlConfiguration.EditApplied += SqlConfiguration_EditApplied;
                    SqlConfiguration.EditCancelled += SqlConfiguration_EditCancelled;
                    SqlConfiguration.ErrorOccured += SqlConfiguration_ErrorOccured;
                    SqlConfiguration.BeginEdit();
                }
            }
        }
        
        private void ApplyEdit()
        {
            SqlConfiguration.ApplyEdit();
        }

        private void CancelEdit()
        {
            SqlConfiguration.CancelEdit();
        }

        private void SqlConfiguration_EditApplied(object sender, EventArgs e)
        {
            Closable.Close();
            NotificationHub.GetInstance().ShowMessage($"{SqlConfiguration.Name} Edited successfuly.");
        }

        private void SqlConfiguration_EditCancelled(object sender, EventArgs e)
        {
            Closable.Close();
        }

        private void SqlConfiguration_ErrorOccured(object sender, PersistibleModel.ErrorOccuredEventArgs e)
        {
            Closable.Close();
            NotificationHub.GetInstance().PopMessage(e.Message, "An error occured");
        }
    }
}
