using ConfigFactory.Core.Attributes;
using System.Reflection;

namespace ConfigFactory.Core.Models;

public class ConfigProperty(PropertyInfo property, ConfigAttribute attribute)
{
    public PropertyInfo Property { get; } = property;
    public ConfigAttribute Attribute { get; } = attribute;

    public void Deconstruct(out PropertyInfo property, out ConfigAttribute attribute)
    {
        property = Property;
        attribute = Attribute;
    }
}
