using CommunityToolkit.Mvvm.ComponentModel;
using ConfigFactory.Core;
using ConfigFactory.Core.Attributes;
using System.Collections.ObjectModel;

namespace ConfigFactory.Avalonia.Demo.Models;

public partial class DemoConfig : ConfigModule<DemoConfig>
{
    [ObservableProperty]
    [property: Config(
        Header = "Some Field",
        Description = "Extended (probably redundant) description of Some Field, a text-based configuration",
        Category = "General",
        Group = "Common")]
    private string _someField = string.Empty;

    [ObservableProperty]
    [property: Config(
        Header = "Bool Field",
        Description = "Extended (probably redundant) description of Bool Field, a toggle-based configuration",
        Category = "General",
        Group = "Common")]
    private bool _boolField = false;

    [ObservableProperty]
    [property: BrowserConfig(
        BrowserMode = BrowserMode.OpenFile,
        Filter = "Backups:*.bak",
        InstanceBrowserKey = "some-browser-field-key")]
    [property: Config(
        Header = "Some Browser Field",
        Description = "Extended (probably redundant) description of Some Field, a text-based configuration",
        Category = "General",
        Group = "Common")]
    private string _someBrowserField = string.Empty;

    [ObservableProperty]
    [property: DropdownConfig(
        RuntimeItemsSourceMethodName = "GetThings")]
    [property: Config(
        Header = "Some Dropdown Field",
        Description = "Extended (probably redundant) description of Some Field, a text-based configuration",
        Category = "General",
        Group = "Common")]
    private string _someDropdownField = string.Empty;

    [ObservableProperty]
    [property: BrowserConfig(BrowserMode = BrowserMode.OpenFile)]
    [property: Config(
        Header = "Some Other Field",
        Description = "Extended (probably redundant) description of Some Field, a text-based configuration",
        Category = "General",
        Group = "Less Common")]
    private string _someOtherField = string.Empty;

    [ObservableProperty]
    [property: BrowserConfig(BrowserMode = BrowserMode.OpenFile)]
    [property: Config(
        Header = "Some Enum Field",
        Description = "Extended (probably redundant) description of Some Enum Field, a enum-based configuration",
        Category = "General",
        Group = "Less Common")]
    private BrowserMode _someEnumField = BrowserMode.SaveFile;

    public static ObservableCollection<string> GetThings()
    {
        return new() {
            { "Entry One" },
            { "Entry Two" },
            { "Entry Three" },
        };
    }
}
