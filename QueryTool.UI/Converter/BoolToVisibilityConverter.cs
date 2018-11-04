using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace QueryTool.UI.Converter
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public bool Inverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value.GetType() == typeof(bool))
            {
                bool visible = (bool)value;
                visible = Inverse ? !visible : visible;
                return (visible) ? Visibility.Visible : Visibility.Collapsed;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.GetType() == typeof(Visibility))
            {
                return ((Visibility)value == Visibility.Visible);
            }
            return Binding.DoNothing;
        }
    }
}
