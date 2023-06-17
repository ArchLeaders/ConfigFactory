using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using ConfigFactory.Core;
using ConfigFactory.Generics;
using System.Reflection;

namespace ConfigFactory.Avalonia.Builders;

public class EnumControlBuilder : ControlBuilder<EnumControlBuilder>
{
    public override object? Build(IConfigModule context, PropertyInfo propertyInfo)
    {
        return new ComboBox {
            DataContext = context,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            ItemsSource = Enum.GetValues(propertyInfo.PropertyType),
            [!SelectingItemsControl.SelectedItemProperty] = new Binding(propertyInfo.Name)
        };
    }

    public override bool IsValid(object? value)
    {
        return value is Enum;
    }
}
