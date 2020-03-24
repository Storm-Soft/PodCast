using System;
using System.Globalization;
using Xamarin.Forms;

namespace Podcast.Mobile.Helpers
{
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
                return TimeSpan.Zero.ToString("hh\\:mm\\:ss");
            return ((TimeSpan)value).ToString("hh\\:mm\\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.Parse(value.ToString());
        }
    }
}
