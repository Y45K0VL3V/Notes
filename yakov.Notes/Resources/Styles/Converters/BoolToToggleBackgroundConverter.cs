using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yakov.Notes.Resources.Styles.Converters
{
    class BoolToToggleBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isEnabled = (bool)value;
            return isEnabled ? new SolidColorBrush(Color.FromArgb("#FF404040")) : new SolidColorBrush(Color.FromArgb("#FF3E8EED"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
