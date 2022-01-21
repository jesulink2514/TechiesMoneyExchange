using System.Globalization;
using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Core.Converter
{
    public class BankTypeToStringConverter : IValueConverter
    {
        public string Savings { get; set; }
        public string Current { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((BankAccountType)value)
            {
                case BankAccountType.Savings:
                    return Savings; 
                case BankAccountType.Current: 
                    return Current;
                default:
                    return "";                    
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
