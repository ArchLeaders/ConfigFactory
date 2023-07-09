using Avalonia.Media;

namespace ConfigFactory.Avalonia.Helpers;

internal static class BrushExtension
{
    public static IImmutableSolidColorBrush AsBrush(this string hex)
    {
        if (hex.StartsWith('#')) {
            BrushConverter converter = new();
            return (IImmutableSolidColorBrush)(converter.ConvertFromString(hex) ?? Brushes.Transparent);
        }

        return Brushes.Transparent;
    }

    public static string AsHexString(this IImmutableSolidColorBrush brush)
    {
        BrushConverter converter = new();
        return converter.ConvertToInvariantString(brush) ?? "#00FFFFFF";
    }
}
