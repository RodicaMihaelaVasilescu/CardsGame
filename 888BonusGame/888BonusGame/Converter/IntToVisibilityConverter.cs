using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace _888BonusGame.Converter
{
  public class IntToVisibilityConverter : IValueConverter
  {
    private int val;
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      val = (int)value;

      return ((val) > 1) ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return null;
    }
  }
}
