using ConfigFactory.Core.Attributes;
using System.Reflection;

namespace ConfigFactory.Core.Models;

public class ConfigProperties : Dictionary<string, (PropertyInfo info, ConfigAttribute attribute)>
{
    public static ConfigProperties Generate<T>()
    {
        return (ConfigProperties)typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(x => (info: x, attribute: x.GetCustomAttribute<ConfigAttribute>()))
            .Where(x => x.attribute != null)
            .ToDictionary(x => x.info.Name, x => (x.info, x.attribute!));
    }

    public static ConfigProperties Generate(Type type)
    {
        return (ConfigProperties)type
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(x => (info: x, attribute: x.GetCustomAttribute<ConfigAttribute>()))
            .Where(x => x.attribute != null)
            .ToDictionary(x => x.info.Name, x => (x.info, x.attribute!));
    }

    public static ConfigProperties Generate(object? context)
    {
        return context != null ? (ConfigProperties)context.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Select(x => (info: x, attribute: x.GetCustomAttribute<ConfigAttribute>()))
            .Where(x => x.attribute != null)
            .ToDictionary(x => x.info.Name, x => (x.info, x.attribute!)) : new();
    }
}
