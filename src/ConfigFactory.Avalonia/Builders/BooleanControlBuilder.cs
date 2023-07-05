using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using ConfigFactory.Core;
using ConfigFactory.Generics;
using System.Reflection;

namespace ConfigFactory.Avalonia.Builders;

public class BooleanControlBuilder : ControlBuilder<BooleanControlBuilder>
{
    public override object? Build(IConfigModule context, PropertyInfo propertyInfo)
    {
        return new ToggleSwitch {
            DataContext = context,
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Right,
            OnContent = string.Empty,
            OffContent = string.Empty,
            [!ToggleButton.IsCheckedProperty] = new Binding(propertyInfo.Name)
        };
    }

    public override bool IsValid(object? value)
    {
        return value is bool;
    }
}
