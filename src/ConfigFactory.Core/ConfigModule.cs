using CommunityToolkit.Mvvm.ComponentModel;
using ConfigFactory.Core.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConfigFactory.Core;

public abstract class ConfigModule<T> : ObservableObject, IConfigModule where T : ConfigModule<T>, new()
{
    IConfigModule IConfigModule.Shared => Shared;
    public static T Shared { get; } = Load();

    /// <summary>
    /// The name of the <see cref="ConfigModule{T}"/>
    /// <para><i>Default: <see langword="nameof(T)"/></i></para>
    /// </summary>
    [JsonIgnore]
    public virtual string Name { get; }

    /// <summary>
    /// The local path of the serialized config file
    /// <para><i>Default: %appdata%/<see langword="nameof(T)"/>/Config.json</i></para>
    /// </summary>
    [JsonIgnore]
    public virtual string LocalPath { get; }

    [JsonIgnore]
    public ConfigProperties Properties { get; }

    public ConfigModule()
    {
        Name = nameof(T);
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
        return JsonSerializer.Deserialize<T>(fs)!;
    }

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(LocalPath)!);
        using FileStream fs = File.Create(LocalPath);
        JsonSerializer.Serialize(fs, (T)this);
    }
}
