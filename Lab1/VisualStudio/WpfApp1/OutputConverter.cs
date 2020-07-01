using Lab4Lib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp1
{
    [ValueConversion(typeof(Person),typeof(string))]
    public class OutputConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Person P = (Person)value;
            string str = "";
            str += P.Name[0] + " " + P.Name[1];
            if (P is Programmer)
                    str += " Programmer";
            if (P is Researcher)
                    str += " Researcher";
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
