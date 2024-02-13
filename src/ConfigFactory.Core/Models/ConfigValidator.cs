namespace ConfigFactory.Core.Models;

public class ConfigValidator(Func<object?, bool> validate)
{
    public Func<object?, bool> Validate { get; set; } = validate;
    public string? ErrorMessage { get; set; }
}
