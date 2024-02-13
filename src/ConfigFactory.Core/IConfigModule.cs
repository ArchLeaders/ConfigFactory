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

    public IConfigModule Shared { get; set; }

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
    /// Resets the <see cref="Properties"/> to their last saved values
    /// </summary>
    /// <returns></returns>
    public void Reset();

    /// <summary>
    /// Saves the current <see cref="IConfigModule"/> instance
    /// </summary>
    public void Save();

    /// <summary>
    /// Validates each property registered in the <see cref="Validators"/> collection
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the validation was successful; <see langword="false"/> if the validation failed
    /// </returns>
    public bool Validate() => Validate(out _, out _);

    /// <inheritdoc cref="Validate()"/>
    public virtual bool Validate(out string? message) => Validate(out message, out _);

    /// <inheritdoc cref="Validate()"/>
    public bool Validate(out string? message, out ConfigProperty target);

    /// <summary>
    /// Translate the string.
    /// </summary>
    /// <param name="input">Input string (key)</param>
    /// <returns>Translated string</returns>
    public string Translate(string input);
}
