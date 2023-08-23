namespace ConfigFactory.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class NumericConfigAttribute : Attribute
{
    /// <summary>
    /// Minimum numeric value
    /// </summary>
    public object? Minimum { get; set; }
    /// <summary>
    /// Maximum numeric value
    /// </summary>
    public object? Maximum { get; set; }
    /// <summary>
    /// Increment numeric value
    /// </summary>
    public object? Increment { get; set; }
}