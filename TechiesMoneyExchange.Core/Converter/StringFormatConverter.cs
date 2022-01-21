using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechiesMoneyExchange.Core.Converter
{
    public class StringFormatConverter : IValueConverter
    {
        public string Format { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format(Format, value?.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
