using ConfigFactory.Core.Components;
using ConfigFactory.Core.Models;
using System.Diagnostics.CodeAnalysis;

namespace ConfigFactory.Core;

/// <summary>
/// Defines a configuration (settings) extension
/// </summary>
public interface IConfigModule
{
    IConfigModule Shared { get; }

    /// <summary>
    /// <see cref="IValidationInterface"/> set by the frontend implementation
    /// </summary>
    IValidationInterface? ValidationInterface { get; set; }

    /// <summary>
    /// Gets or sets the dictionary of property validators
    /// </summary>
    ConfigValidatorCollection Validators { get; }

    /// <summary>
    /// Cached runtime reflected properties used by the UI builder
    /// </summary>
    ConfigPropertyCollection Properties { get; }
        
    /// <summary>
    /// Resets the <see cref="Properties"/> to their last saved values
    /// </summary>
    /// <returns></returns>
    void Reset();

    /// <summary>
    /// Saves the current <see cref="IConfigModule"/> instance
    /// </summary>
    void Save();

    /// <summary>
    /// Validates each property registered in the <see cref="Validators"/> collection
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the validation was successful; <see langword="false"/> if the validation failed
    /// </returns>
    virtual bool Validate() => Validate(out _, out _);

    /// <inheritdoc cref="Validate()"/>
    virtual bool Validate(out string? message) => Validate(out message, out _);

    /// <inheritdoc cref="Validate()"/>
    bool Validate(out string? message, [MaybeNullWhen(true)] out ConfigProperty target);

    /// <summary>
    /// Translate the string.
    /// </summary>
    /// <param name="input">Input string (key)</param>
    /// <returns>Translated string</returns>
    string Translate(string input);
}