using System;
using System.Globalization;
using System.Windows.Data;

namespace DesktopUI.Helpers
{
    public class StringToDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string strVal = value.ToString();

                if (string.IsNullOrEmpty(strVal))
                {
                    return 0;
                }
                else
                {
                    return decimal.Parse(strVal);
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
