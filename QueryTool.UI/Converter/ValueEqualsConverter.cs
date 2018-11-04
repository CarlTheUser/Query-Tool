using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QueryTool.UI.Converter
{
    public class ValueEqualsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (value.GetType() == parameter.GetType()) return parameter.Equals(value);
            //else return false;
            return parameter.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if(value.GetType() == typeof(bool))
            {
                if ((bool)value) return parameter;
            }
            return Binding.DoNothing;
        }
    }
}
