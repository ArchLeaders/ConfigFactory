using ConfigFactory.Avalonia.Models;
using ConfigFactory.Core;

namespace ConfigFactory.Avalonia;

public static class ConfigFactory
{
    public static ConfigPageModel Build<T>() where T : ConfigModule<T>, new() => Build(ConfigModule<T>.Shared);
    public static ConfigPageModel Build<T>(T module) where T : IConfigModule
    {
        ConfigPageModel configPageModel = new();
        configPageModel.Append(module);
        return configPageModel;
    }

    public static ConfigPageModel Append<T>(this ConfigPageModel configPageModel) where T : ConfigModule<T>, new() => Append(configPageModel, ConfigModule<T>.Shared);
    public static ConfigPageModel Append<T>(this ConfigPageModel configPageModel, T module) where T : IConfigModule
    {
        return configPageModel;
    }
}
