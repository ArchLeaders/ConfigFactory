using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ConfigFactory.Models;

public partial class ConfigCategory : ObservableObject
{
    [ObservableProperty]
    private string _header;

    [ObservableProperty]
    private ObservableCollection<ConfigGroup> _groups = new();

    public ConfigCategory(string header)
    {
        _header = header;
    }

    /// <summary>
    /// Custom constructor used in <see cref="ConfigFactory.GetConfigGroup(ConfigPageModel, Core.Attributes.ConfigAttribute)"/> as a helper extension
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="header"></param>
    /// <param name="isFirstCategory"></param>
    internal ConfigCategory(ConfigPageModel parent, string header, out bool isFirstCategory)
    {
        _header = header;
        parent.Categories.Add(this);
        isFirstCategory = true;
    }
}
