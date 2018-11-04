using QueryTool.Data.Client.Data;
using QueryTool.UI.Command;
using QueryTool.UI.Model;
using QueryTool.UI.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QueryTool.UI.Custom
{
    /// <summary>
    /// Interaction logic for QueryListItem.xaml
    /// </summary>
    public partial class QueryListItem : UserControl
    {

        

        public QueryListItem()
        {
            InitializeComponent();
            QueryListItemViewModel vm = new QueryListItemViewModel();
            vm.Query = Query.NewInstance();

            vm.Query.Id = 1;
            vm.Query.Name = "Sample Query";
                vm.Query.Provider = ProviderType.MySql;
                vm.Query.Value = "SELECT P.INUMBR, P.ISORT, P.IDESCR, P.IDEPT, P.ISDEPT, P.ICLAS, P.ISCLAS, D.DPTNAM, SD.DPTNAM AS SUBDEPT, C.DPTNAM AS CLASS, SC.DPTNAM AS SUBCLASS "
                                    + "FROM INVMST P "
                                    + "JOIN INVDPT D "
                                    + "ON P.IDEPT = D.IDEPT "
                                    + "JOIN INVDPT SD "
                                    + "ON P.IDEPT = SD.IDEPT AND P.ISDEPT = SD.ISDEPT "
                                    + "JOIN INVDPT C "
                                    + "ON P.IDEPT = C.IDEPT AND P.ISDEPT = C.ISDEPT AND P.ICLAS = C.ICLAS "
                                    + "JOIN INVDPT SC "
                                    + "ON P.IDEPT = SC.IDEPT AND P.ISDEPT = SC.ISDEPT AND P.ICLAS = SC.ICLAS AND P.ISCLAS = SC.ISCLAS "
                                    + "WHERE(D.ISDEPT = 0 AND D.ICLAS = 0 AND D.ISCLAS = 0) "
                                    + "AND(SD.ICLAS = 0 AND SD.ISCLAS = 0) "
                                    + "AND(C.ISCLAS = 0) "
                                    + "FETCH FIRST 10 ROWS ONLY ";
            
            this.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewPanel.Visibility = Visibility.Collapsed;
            EditPanel.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EditPanel.Visibility = Visibility.Collapsed;
            ViewPanel.Visibility = Visibility.Visible;
        }

        //public class QueryListItemTemplateViewModel : ViewModel.ViewModel
        //{
        //    public ICommand RunCommand { get; private set; }

        //    public ICommand CopyCommand { get; private set; }

        //    public ICommand EditCommand { get; private set; }

        //    public ICommand SaveEditCommand { get; private set; }

        //    public ICommand CancelEditCommand { get; private set; }

        //    public Query Query { get; set; }

        //    private bool _isViewMode = false;

        //    public bool IsViewMode
        //    {
        //        get => _isViewMode;
        //        private set
        //        {
        //            _isViewMode = value;
        //            OnPropertyChanged("IsViewMode");
        //        }
        //    }

        //    public QueryListItemTemplateViewModel()
        //    {
        //        RunCommand = new RelayCommand(RunQuery);
        //        CopyCommand = new RelayCommand(CopyQuery);
        //        EditCommand = new RelayCommand(Edit);
        //        SaveEditCommand = new RelayCommand(SaveEdit);
        //        CancelEditCommand = new RelayCommand(CancelEdit);
        //    }

        //    private void RunQuery()
        //    {
        //        Dictionary<int, object> parameters = new Dictionary<int, object>()
        //        {
        //            { ViewModel.QueryPadViewModel.INITIALQUERY_PARAMETER, Query.Value }
        //        };
        //        App.Current.MainView.UserNavigation.Navigate(new Navigation.NavigationItem(Pages.ApplicationPage.QueryPad, true, parameters));
        //    }

        //    private void CopyQuery()
        //    {
        //        Clipboard.SetText(Query.Value);
        //        NotificationHub.GetInstance().ShowMessage("Query copied to clipboard");
        //    }

        //    private void Edit()
        //    {
        //        Query.BeginEdit();
        //        IsViewMode = true;
        //    }

        //    private void SaveEdit()
        //    {
        //        Query.ApplyEdit();
        //        IsViewMode = false;
        //    }

        //    private void CancelEdit()
        //    {
        //        Query.CancelEdit();
        //        IsViewMode = false;
        //    }
            
        //}

    }
}
