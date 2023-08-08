using CommunityToolkit.Mvvm.ComponentModel;
using ConfigFactory.Avalonia.Demo.Assets;
using ConfigFactory.Core;
using ConfigFactory.Core.Attributes;
using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ConfigFactory.Avalonia.Demo.Models;

public partial class DemoConfig : ConfigModule<DemoConfig>
{
    [ObservableProperty]
    [property: Config(
        Header = "Text_Field",
        Description = "Example_Description",
        Category = "General_Category",
        Group = "Common_Group")]
    private string _someField = string.Empty;

    [ObservableProperty]
    [property: Config(
        Header = "Bool_Field",
        Description = "Example_Description",
        Category = "General_Category",
        Group = "Common_Group")]
    private bool _boolField = false;

    [ObservableProperty]
    [property: BrowserConfig(
        BrowserMode = BrowserMode.OpenFile,
        Filter = "Backups:*.bak",
        InstanceBrowserKey = "some-browser-field-key")]
    [property: Config(
        Header = "Browser_Field",
        Description = "Example_Description",
        Category = "General_Category",
        Group = "Common_Group")]
    private string _someBrowserField = string.Empty;

    [ObservableProperty]
    [property: DropdownConfig(
        RuntimeItemsSourceMethodName = "GetThings")]
    [property: Config(
        Header = "Dropdown_Field",
        Description = "Example_Description",
        Category = "General_Category",
        Group = "Common_Group")]
    private string _someDropdownField = string.Empty;

    [ObservableProperty]
    [property: BrowserConfig(BrowserMode = BrowserMode.OpenFile)]
    [property: Config(
        Header = "Other_Field",
        Description = "This string is not included in the translations resource",
        Category = "General_Category",
        Group = "Uncommon_Group")]
    private string _someOtherField = string.Empty;

    [ObservableProperty]
    [property: BrowserConfig(BrowserMode = BrowserMode.OpenFile)]
    [property: Config(
        Header = "Enum_Field",
        Description = "This string is also not included in the translations resource",
        Category = "General_Category",
        Group = "Uncommon_Group")]
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
    /// Example implementation of i18n using the .resx Embedded Resource
    /// </summary>
    public override string Translate(string input)
    {
        string result = input;
        PropertyInfo? prop = null;

        if (!string.IsNullOrWhiteSpace(input))
        {
            // get resource property
            prop = (typeof(Translations))
                .GetProperty(input, BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
        }

        if (prop != null)
        {
            // use property getter
            var getterInfo = prop.GetGetMethod(nonPublic: true);
            if (getterInfo != null)
            {
                if (Delegate.CreateDelegate(typeof(Func<string>), getterInfo) is Func<string> fn)
                {
                    result = fn();
                }
            }
        }

        return result;
    }
}
