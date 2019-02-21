using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProjectTracker.Converters
{
  public class BoolToColorConverter : IValueConverter
  {
    public object TrueColor { get; set; }
    public object FalseColor { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (!(value is bool))
      {
        return TrueColor;
      }

      return (bool)value ? TrueColor : FalseColor;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
