using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using ConfigFactory.Avalonia.Generics;
using ConfigFactory.Core;
using ConfigFactory.Core.Attributes;
using System.Reflection;

namespace ConfigFactory.Avalonia.Builders;

public class TextControlBuilder : ControlBuilder<TextControlBuilder>
{
    public override object? Build(IConfigModule context, PropertyInfo propertyInfo)
    {
        if (propertyInfo.GetCustomAttribute<BrowserConfigAttribute>() is BrowserConfigAttribute) {
            return new StackPanel {
                DataContext = context,
                Orientation = Orientation.Horizontal,
                Spacing = 5,
                VerticalAlignment = VerticalAlignment.Top,
                Children = {
                    new TextBox {
                        [!TextBox.TextProperty] = new Binding(propertyInfo.Name)
                    },
                    new Button {
                        Width = 20,
                        Height = 20,
                        Content = "..."
                    }
                }
            };
        }
        else if (propertyInfo.GetCustomAttribute<DropdownConfigAttribute>() is DropdownConfigAttribute dca) {
            return new ComboBox {
                DataContext = context,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                ItemsSource = dca.GetItemsSource(context),
                VerticalAlignment = VerticalAlignment.Top,
                [!SelectingItemsControl.SelectedItemProperty] = new Binding(propertyInfo.Name)
            };
        }

        return new TextBox {
            DataContext = context,
            VerticalAlignment = VerticalAlignment.Top,
            [!TextBox.TextProperty] = new Binding(propertyInfo.Name)
        };
    }

    public override bool IsValid(object? value)
    {
        return value is string;
    }
}
