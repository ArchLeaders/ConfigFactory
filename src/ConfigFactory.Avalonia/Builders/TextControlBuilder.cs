using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using ConfigFactory.Avalonia.Generics;
using ConfigFactory.Core;

namespace ConfigFactory.Avalonia.Builders;

public class TextControlBuilder : ControlBuilder<TextControlBuilder>
{
    public override object? Build(IConfigModule context, string propertyName)
    {
        return new TextBox {
            DataContext = context,
            VerticalAlignment = VerticalAlignment.Top,
            [!TextBox.TextProperty] = new Binding(propertyName)
        };
    }

    public override bool IsValid(object? value)
    {
        return value is string;
    }
}
