using ConfigFactory.Core;

namespace ConfigFactory.Avalonia.Generics;

public interface IControlBuilder
{
    public object? Build(IConfigModule context, string propertyName);
    public bool IsValid(object? value);
}
