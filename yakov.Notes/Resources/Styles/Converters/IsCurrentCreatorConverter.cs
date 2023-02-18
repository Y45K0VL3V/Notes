using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yakov.Notes.Resources.Styles.Converters
{
    public class IsCurrentCreatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value.ToString()))
                return true;

            var emailTask = SecureStorage.GetAsync("yakovNotesEmail");
            emailTask.Wait();

            return string.Equals(emailTask.Result, value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
