
using System.Globalization;
using System.Windows.Data;

namespace WpfApp.Converters
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte status)
            {
                return status switch
                {
                    1 => "Active",
                    2 => "Deleted"
                };
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string statusString)
            {
                return statusString == "Active" ? 1 : statusString == "Deleted" ? 2 : 0;
            }
            return 0;
        }
    }
}
