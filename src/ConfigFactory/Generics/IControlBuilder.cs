using ConfigFactory.Core;
using ConfigFactory.Models;
using System.Reflection;

namespace ConfigFactory.Generics;

public interface IControlBuilder
{
    /// <summary>
    /// Generates the custom control stack to be injected into the <see cref="ConfigItem"/>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="propertyInfo"></param>
    /// <returns>The root control in the control stack</returns>
    public object? Build(IConfigModule context, PropertyInfo propertyInfo);

    /// <summary>
    /// Checks whether or not the passed in <paramref name="value"/> is valid for this <see cref="IControlBuilder"/>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool IsValid(object? value);
}
