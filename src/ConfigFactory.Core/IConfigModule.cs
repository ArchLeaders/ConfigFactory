namespace ConfigFactory.Core;

/// <summary>
/// Defines a configuration (settings) extension
/// </summary>
public interface IConfigModule
{
    public IConfigModule Load();
    public void Save();
}
