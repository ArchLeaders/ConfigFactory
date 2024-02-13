using ConfigFactory.Core.Attributes;
using System.Reflection;

namespace ConfigFactory.Core.Models;

public class ConfigPropertyCollection : Dictionary<string, ConfigProperty>
{
    public static ConfigPropertyCollection Generate<T>()
    {
        return new(typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(x => (info: x, attribute: x.GetCustomAttribute<ConfigAttribute>()))
            .Where(x => x.attribute != null));
    }

    public static ConfigPropertyCollection Generate(Type type)
    {
        return new(type
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(x => (info: x, attribute: x.GetCustomAttribute<ConfigAttribute>()))
            .Where(x => x.attribute != null));
    }

    public static ConfigPropertyCollection Generate(object? context)
    {
        return context != null ? new(context.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(x => (info: x, attribute: x.GetCustomAttribute<ConfigAttribute>()))
            .Where(x => x.attribute != null)) : new();
    }

    public ConfigPropertyCollection() { }
    private ConfigPropertyCollection(IEnumerable<(PropertyInfo info, ConfigAttribute? attribute)> map)
    {
        foreach ((var info, var attribute) in map) {
            Add(info.Name, new(info, attribute!));
        }
    }
}
