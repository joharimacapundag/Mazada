using System;
using System.Globalization;
using System.Windows.Data;

namespace Mazada.Services
{
    public class UsdToPhpConverter : IValueConverter
    {
        public double ExchangeRate { get; set; } = 56.5;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double usd;

            if (value is decimal dec)
                usd = (double)dec;
            else if (value is double d)
                usd = d;
            else
                return value; 

            double php = usd * ExchangeRate;
            return $"₱{php:N2}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
