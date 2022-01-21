using System.Globalization;

namespace TechiesMoneyExchange.Core.Converter
{
    public class NegatedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var originalValue = (bool)value;
            return !originalValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var originalValue = (bool)value;
            return !originalValue;
        }
    }
}
