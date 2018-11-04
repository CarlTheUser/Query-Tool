using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QueryTool.UI.Converter
{
    class PasswordMaskConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Binding.DoNothing;
            if (value.GetType() == typeof(string))
            {
                string toMask = (string)value;

                if(toMask.Length > 0)
                {
                    string lower = toMask.ToLower();

                    bool isPassword = lower.Contains("password");
                    bool isPwd = lower.Contains("pwd");
                    bool isPass = lower.Contains("pass");


                    bool hasPassword = isPassword || isPwd || isPass;

                    if (hasPassword)
                    {
                        string[] values = toMask.Split(';');

                        string passwordMark = isPassword ? "password" : isPwd ? "pwd" : isPass ? "pass" : "";

                        int size = values.Length;

                        for (int i = 0; i < size; i++)
                        {
                            if (values[i].ToLower().Contains(passwordMark))
                            {
                                int index = values[i].IndexOf('=') + 1;
                                string password = values[i].Substring(index);

                                if (password.Length > 0)
                                {
                                    string passwordMask = values[i].Replace(password, new string('*', password.Length));
                                    values[i] = passwordMask;

                                    string maskedPassword = string.Join(";", values);

                                    return maskedPassword;
                                }
                                break;
                            }
                        }

                    }
                    
                }
                return value;

            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
