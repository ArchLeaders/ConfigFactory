using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using CommunityToolkit.Mvvm.Input;
using ConfigFactory.Avalonia.Helpers;
using ConfigFactory.Core;
using ConfigFactory.Core.Attributes;
using ConfigFactory.Generics;
using System.Reflection;

namespace ConfigFactory.Avalonia.Builders;

public class TextControlBuilder : ControlBuilder<TextControlBuilder>
{
    public override object? Build(IConfigModule context, PropertyInfo propertyInfo)
    {
        if (propertyInfo.GetCustomAttribute<BrowserConfigAttribute>() is BrowserConfigAttribute browserConfigAttribute) {
            return new Grid {
                DataContext = context,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Top,
                Children = {
                    new TextBox {
                        Margin = new(0, 0, 37, 0),
                        [!TextBox.TextProperty] = new Binding(propertyInfo.Name)
                    },
                    new Button {
                        Content = "...",
                        DataContext = new {
                            RelayCommand = new RelayCommand(async () => {
                                BrowserDialog dialog = new(
                                    browserConfigAttribute.BrowserMode,
                                    browserConfigAttribute.Title,
                                    browserConfigAttribute.Filter,
                                    browserConfigAttribute.SuggestedFileName,
                                    browserConfigAttribute.InstanceBrowserKey);
                                if (await dialog.ShowDialog() is string value) {
                                    propertyInfo.SetValue(context, value);
                                }
                            })
                        },
                        HorizontalAlignment = HorizontalAlignment.Right,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        Width = 32,
                        [!Button.CommandProperty] = new Binding("RelayCommand")
                    }
                }
            };
        }
        else if (propertyInfo.GetCustomAttribute<DropdownConfigAttribute>() is DropdownConfigAttribute dca) {
            return DropdownBuilder.Build(context, propertyInfo, dca);
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
