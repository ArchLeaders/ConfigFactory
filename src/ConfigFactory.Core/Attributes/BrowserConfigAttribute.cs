namespace ConfigFactory.Core.Attributes;

public enum BrowserMode
{
    None,
    OpenFile,
    OpenFolder,
    SaveFile
}

[AttributeUsage(AttributeTargets.Property)]
public class BrowserConfigAttribute : Attribute
{
    public BrowserMode BrowserMode { get; set; }
}
