using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Interface.Helpers
{
    class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Brushes.Transparent;

            string status = value.ToString();
            switch (status)
            {
                case "Needs Designer":
                case "Needs Tailor":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6554C0"));

                case "Proposed by Designer":
                case "Proposed by Tailor":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAB00"));

                case "On Progress by Designer":
                case "On Progress by Tailor":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0065FF"));

                case "Finished by Designer":
                case "Finished by Tailor":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#36B37E"));

                default:
                    return Brushes.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
