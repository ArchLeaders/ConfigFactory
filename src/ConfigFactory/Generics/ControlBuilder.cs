using ConfigFactory.Core;
using System.Reflection;

namespace ConfigFactory.Generics;

public abstract class ControlBuilder<T> : IControlBuilder where T : ControlBuilder<T>, new()
{
    public static T Shared { get; } = new();

    public abstract object? Build(IConfigModule context, PropertyInfo propertyInfo);
    public abstract bool IsValid(Type type);
}
