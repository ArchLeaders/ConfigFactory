using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using ConfigFactory.Core;
using ConfigFactory.Core.Attributes;
using System.Reflection;

namespace ConfigFactory.Avalonia.Helpers;

/// <summary>
/// The DropdownBuilder is a static helper and doesn't implement <see cref="Generics.ControlBuilder{T}"/>.
/// <br/> Dropdown is an optional control type for any property but not the base.
/// </summary>
internal static class DropdownBuilder
{
    public static ComboBox Build(IConfigModule context, PropertyInfo propertyInfo, DropdownConfigAttribute dca)
    {
        ComboBox comboBox = new() {
            DataContext = context,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            ItemsSource = dca.GetItemsSource(context),
            VerticalAlignment = VerticalAlignment.Top,
        };

        if (!string.IsNullOrWhiteSpace(dca.DisplayMemberPath)) {
            comboBox.DisplayMemberBinding = new Binding(dca.DisplayMemberPath);
        }

        if (!string.IsNullOrWhiteSpace(dca.SelectedValuePath)) {
            comboBox.SelectedValueBinding = new Binding(dca.SelectedValuePath);
            comboBox[!SelectingItemsControl.SelectedValueProperty] = new Binding(propertyInfo.Name, BindingMode.TwoWay);
        }
        else {
            comboBox[!SelectingItemsControl.SelectedItemProperty] = new Binding(propertyInfo.Name);
        }

        return comboBox;
    }
}
