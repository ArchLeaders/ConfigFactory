using Avalonia.Data.Converters;
using Avalonia.Media;
using ConfigFactory.Avalonia.Helpers;
using System.Globalization;

namespace ConfigFactory.Avalonia.Converters;

internal class BrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string hex) {
            return hex.AsBrush();
        }

        return Brushes.Transparent;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
