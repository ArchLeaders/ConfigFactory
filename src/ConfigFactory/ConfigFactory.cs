using ConfigFactory.Core;
using ConfigFactory.Core.Attributes;
using ConfigFactory.Generics;
using ConfigFactory.Models;
using System.Reflection;

namespace ConfigFactory;

public static class ConfigFactory
{
    private static readonly List<IControlBuilder> _builders = new();

    /// <summary>
    /// Constructs a new <see cref="ConfigPageModel"/> object with the configuration items found in the <see cref="IConfigModule"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ConfigPageModel Build<T>() where T : ConfigModule<T>, new() => Build(ConfigModule<T>.Shared);

    /// <inheritdoc cref="Build{T}"/>
    public static ConfigPageModel Build(IConfigModule module)
    {
        ConfigPageModel configPageModel = new();
        configPageModel.Append(module);
        return configPageModel;
    }

    /// <summary>
    /// Appends the configuration items found in the <see cref="IConfigModule"/> to the <paramref name="configPageModel"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configPageModel"></param>
    /// <returns></returns>
    public static ConfigPageModel Append<T>(this ConfigPageModel configPageModel) where T : ConfigModule<T>, new() => Append(configPageModel, ConfigModule<T>.Shared);

    /// <inheritdoc cref="Append{T}(ConfigPageModel)"/>
    public static ConfigPageModel Append(this ConfigPageModel configPageModel, IConfigModule module)
    {
        configPageModel.PrimaryButtonEvent += () => {
            module.Save();
            return Task.CompletedTask;
        };

        foreach ((_, (PropertyInfo info, ConfigAttribute attribute)) in module.Properties) {
            object? value = info.GetValue(module, null);
            if (_builders.FirstOrDefault(x => x.IsValid(value)) is IControlBuilder builder) {
                ConfigGroup group = GetConfigGroup(configPageModel, attribute);
                group.Items.Add(new ConfigItem {
                    Content = builder.Build(module, info),
                    Description = attribute.Description,
                    Header = attribute.Header
                });
            }
        }

        return configPageModel;
    }

    /// <summary>
    /// Registers a custom <see cref="IControlBuilder"/> to handle custom property types
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void RegisterBuilder<T>() where T : ControlBuilder<T>, new() => Register(ControlBuilder<T>.Shared);

    /// <inheritdoc cref="RegisterBuilder{T}"/>
    public static void Register(this IControlBuilder builder)
    {
        _builders.Add(builder);
    }

    /// <summary>
    /// Locates the <see cref="ConfigGroup"/> matching the <paramref name="attribute"/> properties
    /// </summary>
    /// <param name="configPageModel"></param>
    /// <param name="attribute"></param>
    /// <returns></returns>
    private static ConfigGroup GetConfigGroup(ConfigPageModel configPageModel, ConfigAttribute attribute)
    {
        ConfigCategory category = configPageModel.Categories.FirstOrDefault(
            x => x.Header == attribute.Category) is ConfigCategory _category
            ? _category : new(parent: configPageModel, attribute.Category);

        ConfigGroup group = category.Groups.FirstOrDefault(
            x => x.Header == attribute.Group) is ConfigGroup _group
            ? _group : new(parent: category, attribute.Group);

        if (configPageModel.Categories.Count == 1 && configPageModel.Categories[0].Groups.Count == 1) {
            configPageModel.SelectedGroup = group;
        }

        return group;
    }
}
