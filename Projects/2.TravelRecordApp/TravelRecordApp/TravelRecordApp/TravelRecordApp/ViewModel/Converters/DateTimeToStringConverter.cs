using System;
using System.Globalization;
using Xamarin.Forms;

namespace TravelRecordApp.ViewModel.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeAgo = string.Empty;

            var currentPostDateTime = (DateTimeOffset) value;
            var rightNow = DateTimeOffset.Now;

            var difference = rightNow - currentPostDateTime;

            if (difference.TotalDays > 1) return $"{currentPostDateTime:d}";

            if (difference.TotalSeconds < 60) return $"{difference.TotalSeconds:0} seconds ago";

            if (difference.TotalMinutes < 60) return $"{difference.TotalMinutes:0} minutes ago";

            if (difference.TotalHours < 24) return $"{difference.TotalHours:0} hours ago";

            return "yesterday";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTimeOffset.Now;
        }
    }
}