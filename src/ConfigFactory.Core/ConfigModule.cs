using System.Text.Json;

namespace ConfigFactory.Core;

public abstract class ConfigModule<T> : IConfigModule where T : ConfigModule<T>, new()
{
    public static T Shared { get; } = Load();
    public virtual string Name { get; } = nameof(T);
    public virtual string LocalPath { get; }

    public ConfigModule()
    {
        LocalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Name, "config.json");
    }

    public static T Load() => (T)(new T() as IConfigModule).Load();
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
