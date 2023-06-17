using ConfigFactory.Core;
using System.Reflection;

namespace ConfigFactory.Avalonia.Generics;

public interface IControlBuilder
{
    public object? Build(IConfigModule context, PropertyInfo propertyInfo);
    public bool IsValid(object? value);
}
