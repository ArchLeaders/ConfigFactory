using ConfigFactory.Core.Attributes;
using System.Reflection;

namespace ConfigFactory.Core.Models;

public class ConfigProperties : Dictionary<string, (PropertyInfo info, ConfigAttribute attribute)>
{
    public static ConfigProperties Generate<T>()
    {
        return new(typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(x => (info: x, attribute: x.GetCustomAttribute<ConfigAttribute>()))
            .Where(x => x.attribute != null));
    }

    public static ConfigProperties Generate(Type type)
    {
        return new(type
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(x => (info: x, attribute: x.GetCustomAttribute<ConfigAttribute>()))
            .Where(x => x.attribute != null));
    }

    public static ConfigProperties Generate(object? context)
    {
        return context != null ? new(context.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(x => (info: x, attribute: x.GetCustomAttribute<ConfigAttribute>()))
            .Where(x => x.attribute != null)) : new();
    }

    public ConfigProperties() { }
    private ConfigProperties(IEnumerable<(PropertyInfo info, ConfigAttribute? attribute)> map)
    {
        foreach ((var info, var attribute) in map) {
            Add(info.Name, (info, attribute!));
        }
    }
}
