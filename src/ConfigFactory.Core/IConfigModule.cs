using ConfigFactory.Core.Models;

namespace ConfigFactory.Core;

/// <summary>
/// Defines a configuration (settings) extension
/// </summary>
public interface IConfigModule
{
    public IConfigModule Shared { get; }

    /// <summary>
    /// Cached runtime reflected properties used by the UI builder
    /// </summary>
    public ConfigProperties Properties { get; }

    /// <summary>
    /// Reads the <see cref="IConfigModule"/> from the saved location and returns the result
    /// </summary>
    /// <returns></returns>
    public IConfigModule Load();

    /// <summary>
    /// Saves the current <see cref="IConfigModule"/> instance
    /// </summary>
    public void Save();
}
