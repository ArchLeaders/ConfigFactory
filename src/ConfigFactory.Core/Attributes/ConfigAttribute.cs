namespace ConfigFactory.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ConfigAttribute : Attribute
{
    public required string Header { get; set; }
    public required string Description { get; set; }
}
