using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yakov.Notes.Resources.Styles.Converters
{
    public class CurrentCreatorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value.ToString()))
                return true;

            var emailTask = SecureStorage.GetAsync("yakovNotesEmail");
            emailTask.Wait();

            return string.Equals(emailTask.Result, value.ToString()) ? new SolidColorBrush(Color.FromArgb("#FF3E8EED")) : new SolidColorBrush(Color.FromArgb("#00000000"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
