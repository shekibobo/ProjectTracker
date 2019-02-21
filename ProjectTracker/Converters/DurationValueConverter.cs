using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProjectTracker.Converters
{
  public class DurationValueConverter: IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var durationSeconds = (int)value;
      return TimeSpan.FromSeconds(durationSeconds).ToString("c");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
