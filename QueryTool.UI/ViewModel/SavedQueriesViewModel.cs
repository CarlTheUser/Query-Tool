using QueryTool.Data.Client.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.UI.ViewModel
{
    public class SavedQueriesViewModel : ViewModel
    {
        public ObservableCollection<QueryData> Queries { get; private set; }

        public SavedQueriesViewModel()
        {
            List<QueryData> queries = new List<QueryData>()
            {
                new QueryData
                {
                    Id = 1,
                    Name = "Products Base Query",
                    Provider = ProviderType.MySql,
                    Value = "SELECT P.Id P.Name, P.Brand, P.Quantity FROM Products P"
                },
                new QueryData
                {
                    Id = 1,
                    Name = "Users Query",
                    Provider = ProviderType.MySql,
                    Value = "SELECT U.Id U.Name, U.Password, U.Birthdate FROM Users U"
                },
                new QueryData
                {
                    Id = 1,
                    Name = "Users Query",
                    Provider = ProviderType.MySql,
                    Value = "SELECT U.Id U.Name, U.Password, U.Birthdate FROM Users U"
                }

            };

            Queries = new ObservableCollection<QueryData>(queries);
        }
    }
}
