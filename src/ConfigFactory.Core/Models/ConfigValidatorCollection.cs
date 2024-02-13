namespace ConfigFactory.Core.Models;

public class ConfigValidatorCollection : Dictionary<string, (Func<object?, bool>, string?)>
{

}
