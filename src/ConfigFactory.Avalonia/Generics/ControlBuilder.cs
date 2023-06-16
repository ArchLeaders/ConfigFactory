using ConfigFactory.Core;

namespace ConfigFactory.Avalonia.Generics;

public abstract class ControlBuilder<T> : IControlBuilder where T : ControlBuilder<T>, new()
{
    public static T Shared { get; } = new();

    public abstract object? Build(IConfigModule context, string propertyName);
    public abstract bool IsValid(object? value);
}
