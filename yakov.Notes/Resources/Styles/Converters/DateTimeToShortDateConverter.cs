using Kotlin.Jvm.Functions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Provider.Telephony;

namespace yakov.Notes.Resources.Styles.Converters
{
    class DateTimeToShortDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? dateTime = value as DateTime?;
            if (!dateTime.HasValue) 
                return null;

            if ((DateTime.Now - dateTime.Value).TotalDays < 1)
                return dateTime.Value.ToString("HH:mm");

            if (DateTime.Now.Year == dateTime.Value.Year)
                return dateTime.Value.ToString("dd MMM");
            else
                return dateTime.Value.ToString("dd MMM yyyy");

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
