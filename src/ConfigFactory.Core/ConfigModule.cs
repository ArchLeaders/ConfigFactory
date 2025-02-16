using CommunityToolkit.Mvvm.ComponentModel;
using ConfigFactory.Core.Components;
using ConfigFactory.Core.Models;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConfigFactory.Core;

public abstract class ConfigModule<T> : ObservableObject, IConfigModule where T : ConfigModule<T>, new()
{
    private static readonly Lazy<T> _shared = new(() => {
        T module = new();
        module.Load(ref module);
        return module;
    });

    protected virtual string SuccessColor { get; } = "#FF31C059";
    protected virtual string FailureColor { get; } = "#FFE64032";

    IConfigModule IConfigModule.Shared => Shared;
    public static T Shared { get; set; } = _shared.Value;

    [JsonIgnore]
    public IValidationInterface? ValidationInterface { get; set; }

    /// <summary>
    /// The name of the <see cref="ConfigModule{T}"/>
    /// <para><i>Default: <see langword="typeof(T).Name"/></i></para>
    /// </summary>
    [JsonIgnore]
    public virtual string Name { get; }

    /// <summary>
    /// The local path of the serialized config file
    /// <para><i>Default: %localappdata%/<see langword="typeof(T).Name"/>/Config.json</i></para>
    /// </summary>
    [JsonIgnore]
    public virtual string LocalPath { get; }

    [JsonIgnore]
    public ConfigPropertyCollection Properties { get; }

    [JsonIgnore]
    public ConfigValidatorCollection Validators { get; } = [];

    /// <summary>
    /// Executed before saving; return <see langword="false"/> to stop saving
    /// </summary>
    public event Func<bool> OnSaving = () => true;

    /// <summary>
    /// Executed after saving
    /// </summary>
    public event Action OnSave = () => { };

    public ConfigModule()
    {
        Name = typeof(T).Name;
        LocalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Name, "Config.json");
        Properties = ConfigPropertyCollection.Generate<T>();
    }

    void IConfigModule.Load(ref IConfigModule module)
    {
        if (module is T explcitModule) {
            Load(ref explcitModule);
            module = explcitModule;
        }
    }

    public virtual void Load(ref T module)
    {
        if (File.Exists(module.LocalPath)) {
            using FileStream fs = File.OpenRead(module.LocalPath);
            module = JsonSerializer.Deserialize<T>(fs)!;
        }

        foreach ((string name, (PropertyInfo property, _)) in module.Properties) {
            if (typeof(T).GetMethod($"On{name}Changed", BindingFlags.NonPublic | BindingFlags.Instance)
                    is { } onChanged) {
                switch (onChanged.GetParameters().Length) {
                    case 1:
                        onChanged.Invoke(module, [property.GetValue(module)]);
                        break;
                    case 2:
                        onChanged.Invoke(module, [null, property.GetValue(module)]);
                        break;
                }
            }
        }
    }

    public virtual void Reset()
    {
        T config = new();
        Load(ref config);

        foreach (var (name, (property, _)) in Properties) {
            property.SetValue(Shared, config.Properties[name].Property.GetValue(config));
        }
    }

    public virtual void Save()
    {
        if (OnSaving()) {
            Directory.CreateDirectory(Path.GetDirectoryName(LocalPath)!);
            using FileStream fs = File.Create(LocalPath);
            JsonSerializer.Serialize(fs, (T)this);
            OnSave();
        }
    }

    public virtual bool Validate(out string? message, [MaybeNullWhen(true)] out ConfigProperty target)
    {
        foreach (var (name, (validate, errorMessage)) in Validators) {
            target = Properties[name];
            PropertyInfo propertyInfo = target.Property;
            if (validate(propertyInfo.GetValue(this)) is bool isValid) {
                ValidationInterface?.SetValidationColor(propertyInfo, isValid ? SuccessColor : FailureColor);

                if (!isValid) {
                    message = errorMessage;
                    return false;
                }
            }
        }

        target = default;
        message = "Validation Successful";
        return true;
    }

    protected virtual void Validate<TProperty>(Expression<Func<TProperty>> property, Func<TProperty?, bool> validation,
        string? invalidErrorMessage = null, string? validationFailureColor = null, string? validationSuccessColor = null)
    {
        PropertyInfo propertyInfo = (PropertyInfo)((MemberExpression)property.Body).Member;

        Validators.TryAdd(propertyInfo.Name,
            (x => validation((TProperty?)x), invalidErrorMessage));

        ValidationInterface?.SetValidationColor(propertyInfo,
            validation(property.Compile()()) ? validationSuccessColor ?? SuccessColor : validationFailureColor ?? FailureColor);
    }

    /// <summary>
    /// Default implementation of the <see cref="IConfigModule.Translate(string)"/>.
    /// <br/>Returns the <paramref name="input"/> string.
    /// </summary>
    /// <param name="input">Input string (key)</param>
    /// <returns><paramref name="input"/></returns>
    public virtual string Translate(string input)
    {
        return input;
    }
}
