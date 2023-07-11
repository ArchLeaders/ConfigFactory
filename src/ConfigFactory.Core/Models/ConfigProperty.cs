using ConfigFactory.Core.Attributes;
using System.Reflection;

namespace ConfigFactory.Core.Models;

public readonly struct ConfigProperty
{
    public PropertyInfo Property { get; }
    public ConfigAttribute Attribute { get; }

    public void Deconstruct(out PropertyInfo property, out ConfigAttribute attribute)
    {
        property = Property;
        attribute = Attribute;
    }

    public ConfigProperty(PropertyInfo property, ConfigAttribute attribute)
    {
        Property = property;
        Attribute = attribute;
    }
}
