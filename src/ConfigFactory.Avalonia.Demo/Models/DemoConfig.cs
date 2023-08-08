using CommunityToolkit.Mvvm.ComponentModel;
using ConfigFactory.Avalonia.Demo.Assets;
using ConfigFactory.Core;
using ConfigFactory.Core.Attributes;
using System.Collections.ObjectModel;

namespace ConfigFactory.Avalonia.Demo.Models;

public partial class DemoConfig : ConfigModule<DemoConfig>
{
    [ObservableProperty]
    [property: Config(
        Header = "Text Field",
        Description = "Example Description",
        Category = "General",
        Group = "Common")]
    private string _someField = string.Empty;

    [ObservableProperty]
    [property: Config(
        Header = "Bool Field",
        Description = "Example Description",
        Category = "General",
        Group = "Common")]
    private bool _boolField = false;

    [ObservableProperty]
    [property: BrowserConfig(
        BrowserMode = BrowserMode.OpenFile,
        Filter = "Backups:*.bak",
        InstanceBrowserKey = "some-browser-field-key")]
    [property: Config(
        Header = "Browser Field",
        Description = "Example Description",
        Category = "General",
        Group = "Common")]
    private string _someBrowserField = string.Empty;

    [ObservableProperty]
    [property: DropdownConfig(
        RuntimeItemsSourceMethodName = "GetThings")]
    [property: Config(
        Header = "Dropdown Field",
        Description = "Example Description",
        Category = "General",
        Group = "Common")]
    private string _someDropdownField = string.Empty;

    [ObservableProperty]
    [property: BrowserConfig(BrowserMode = BrowserMode.OpenFile)]
    [property: Config(
        Header = "Other Field",
        Description = "This string is not included in the translations resource",
        Category = "General",
        Group = "Less Common")]
    private string _someOtherField = string.Empty;

    [ObservableProperty]
    [property: BrowserConfig(BrowserMode = BrowserMode.OpenFile)]
    [property: Config(
        Header = "Enum Field",
        Description = "This string is also not included in the translations resource",
        Category = "General",
        Group = "Less Common")]
    private BrowserMode _someEnumField = BrowserMode.SaveFile;

    public static ObservableCollection<string> GetThings(IConfigModule context)
    {
        return new() {
            { "Entry One" },
            { "Entry Two" },
            { "Entry Three" },
        };
    }

    partial void OnBoolFieldChanged(bool value)
    {
        SetValidation(() => BoolField, value => {
            return value is true;
        });
    }

    /// <summary>
    /// Example implementation of internationalization and localization using the .resx Embedded Resource
    /// </summary>
    public override string Translate(string input)
    {

        if (string.IsNullOrWhiteSpace(input))
            return input;

        return Translations.ResourceManager?.GetString(input, Translations.Culture) ?? input;
    }
}
