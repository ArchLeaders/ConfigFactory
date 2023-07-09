using ConfigFactory.Core.Components;
using ConfigFactory.Core.Models;

namespace ConfigFactory.Core;

/// <summary>
/// Defines a configuration (settings) extension
/// </summary>
public interface IConfigModule
{
    /// <summary>
    /// <see cref="IValidationInterface"/> set by the frontend implementation
    /// </summary>
    public IValidationInterface? ValidationInterface { get; set; }

    /// <summary>
    /// Gets or sets the dictionary of property validators
    /// </summary>
    public Dictionary<string, (Func<object?, bool>, string?)> Validators { get; }

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

    /// <summary>
    /// Validates the properties and sends the invalid error message to the output <paramref name="message"/>
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the validation was successful; <see langword="false"/> if the validation failed
    /// </returns>
    public bool Validate(out string? message);
}
