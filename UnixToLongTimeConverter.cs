using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PsApp
{
    class UnixToLongTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            if (value is long l)
            {
                return epoch.AddSeconds(l).ToLocalTime().ToLongTimeString();
            }
            else
            {
                // cannot convert, return the given value as-is
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        { 
            return value;
        }
    }
}
