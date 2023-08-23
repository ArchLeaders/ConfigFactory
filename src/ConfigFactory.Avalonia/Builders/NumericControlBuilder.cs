using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using ConfigFactory.Avalonia.Helpers;
using ConfigFactory.Core;
using ConfigFactory.Core.Attributes;
using ConfigFactory.Generics;
using System.Reflection;

namespace ConfigFactory.Avalonia.Builders;

public class NumericControlBuilder : ControlBuilder<NumericControlBuilder>
{
    public override object? Build(IConfigModule context, PropertyInfo propertyInfo)
    {
        if (propertyInfo.GetCustomAttribute<DropdownConfigAttribute>() is DropdownConfigAttribute dropdownConfigAttribute) {
            return DropdownBuilder.Build(context, propertyInfo, dropdownConfigAttribute);
        }

        NumericUpDown control = new() {
            DataContext = context,
            VerticalAlignment = VerticalAlignment.Top,
            [!NumericUpDown.ValueProperty] = new Binding(propertyInfo.Name)
        };

        if (propertyInfo.GetCustomAttribute<NumericConfigAttribute>() is NumericConfigAttribute numericConfigAttribute) {
            if (TryConvertToDecimal(numericConfigAttribute.Minimum, out decimal min)) {
                control.Minimum = min;
            }
            if (TryConvertToDecimal(numericConfigAttribute.Maximum, out decimal max)) {
                control.Maximum = max;
            }
            if (TryConvertToDecimal(numericConfigAttribute.Increment, out decimal inc)) {
                control.Increment = inc;
            }
        }

        return control;
    }

    public override bool IsValid(Type type)
    {
        return type.IsPrimitive &&
            type != typeof(bool) || type != typeof(char);
    }

    private static bool TryConvertToDecimal(object? value, out decimal result)
    {
        try {
            result = Convert.ToDecimal(value);
            return true;
        }
        catch {
            result = 0;
            return false;
        }
    }
}