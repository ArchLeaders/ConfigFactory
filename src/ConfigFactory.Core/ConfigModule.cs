using CommunityToolkit.Mvvm.ComponentModel;
using ConfigFactory.Core.Components;
using ConfigFactory.Core.Models;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConfigFactory.Core;

public abstract class ConfigModule<T> : ObservableObject, IConfigModule where T : ConfigModule<T>, new()
{
    protected virtual string SuccessColor { get; } = "#FF31C059";
    protected virtual string FailureColor { get; } = "#FFE64032";

    IConfigModule IConfigModule.Shared {
        get => Shared;
        set => Shared = (T)value;
    }

    public static T Shared { get; set; } = Load();

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
    /// <para><i>Default: %appdata%/<see langword="typeof(T).Name"/>/Config.json</i></para>
    /// </summary>
    [JsonIgnore]
    public virtual string LocalPath { get; }

    [JsonIgnore]
    public ConfigProperties Properties { get; }

    [JsonIgnore]
    public Dictionary<string, (Func<object?, bool>, string?)> Validators { get; } = new();

    public ConfigModule()
    {
        Name = typeof(T).Name;
        LocalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Name, "Config.json");
        Properties = ConfigProperties.Generate<T>();
    }

    private static T Load() => (T)(new T() as IConfigModule).Load();
    IConfigModule IConfigModule.Load()
    {
        if (!File.Exists(LocalPath)) {
            T config = new();
            config.Save();
            return config;
        }

        using FileStream fs = File.OpenRead(LocalPath);
        T result = JsonSerializer.Deserialize<T>(fs)!;

        object?[] parameters = { null };
        foreach ((var name, _) in Properties) {
            typeof(T).GetMethod($"On{name}Changed", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(result, parameters);
        }

        return result;
    }

    public void Reset()
    {
        IConfigModule config = Load();
        foreach ((var name, (var property, _)) in Properties) {
            property.SetValue(Shared, config.Properties[name].Property.GetValue(config));
        }
    }

    protected void SetValidation<TProperty>(Expression<Func<TProperty>> property, Func<TProperty?, bool> validation,
        string? invalidErrorMessage = null, string? validationFailureColor = null, string? validationSuccessColor = null)
    {
        PropertyInfo propertyInfo = (PropertyInfo)((MemberExpression)property.Body).Member;

        Validators.TryAdd(propertyInfo.Name,
            (x => validation((TProperty?)x), invalidErrorMessage));

        ValidationInterface?.SetValidationColor(propertyInfo,
            validation(property.Compile()()) ? validationSuccessColor ?? SuccessColor : validationFailureColor ?? FailureColor);
    }

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(LocalPath)!);
        using FileStream fs = File.Create(LocalPath);
        JsonSerializer.Serialize(fs, (T)this);
    }

    public bool Validate() => Validate(out _, out _);
    public bool Validate(out string? message) => Validate(out message, out _);
    public bool Validate(out string? message, out ConfigProperty target)
    {
        foreach ((var name, (var validate, var errorMessage)) in Validators) {
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

        target = new();
        message = "Validation Successful";
        return true;
    }
}
