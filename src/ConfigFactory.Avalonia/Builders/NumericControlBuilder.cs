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
        // build as drop down
        if (propertyInfo.GetCustomAttribute<DropdownConfigAttribute>() is DropdownConfigAttribute dca)
        {
            return DropdownBuilder.Build(context, propertyInfo, dca);
        }

        // build as numeric control
        var control = new NumericUpDown
        {
            DataContext = context,
            VerticalAlignment = VerticalAlignment.Top,
            [!NumericUpDown.ValueProperty] = new Binding(propertyInfo.Name)
        };

        if (propertyInfo.GetCustomAttribute<NumericConfigAttribute>() is NumericConfigAttribute nca)
        {
            if (TryConvertToDecimal(nca.Minimum, out decimal min))
            {
                control.Minimum = min;
            }
            if (TryConvertToDecimal(nca.Maximum, out decimal max))
            {
                control.Maximum = max;
            }
            if (TryConvertToDecimal(nca.Increment, out decimal inc))
            {
                control.Increment = inc;
            }
        }

        return control;
    }

    public override bool IsValid(object? value)
    {
        return value is sbyte
            || value is byte
            || value is short
            || value is ushort
            || value is int
            || value is uint
            || value is long
            || value is ulong
            || value is float
            || value is double
            || value is decimal;
    }

    private bool TryConvertToDecimal(object? value, out decimal result)
    {
        try
        {
            result = Convert.ToDecimal(value);
            return true;
        }
        catch
        {
            result = 0;
            return false;
        }
    }
}