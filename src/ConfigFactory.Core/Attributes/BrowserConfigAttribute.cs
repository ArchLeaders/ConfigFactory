namespace ConfigFactory.Core.Attributes;

public enum BrowserMode
{
    OpenFile,
    OpenFolder,
    SaveFile
}

[AttributeUsage(AttributeTargets.Property)]
public class BrowserConfigAttribute : Attribute
{
    public required BrowserMode BrowserMode { get; set; }
    public string? Title { get; set; }
    public string? Filter { get; set; }
    public string? SuggestedFileName { get; set; }
    public string? InstanceBrowserKey { get; set; }
}
