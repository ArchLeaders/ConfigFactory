using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ConfigFactory.Models;

public partial class ConfigCategory : ObservableObject
{
    /// <summary>
    /// Internal key
    /// </summary>
    [ObservableProperty]
    private string _id;

    /// <summary>
    /// Displayed header
    /// </summary>
    [ObservableProperty]
    private string _header;

    [ObservableProperty]
    private ObservableCollection<ConfigGroup> _groups = new();

    public ConfigCategory(string id, string header)
    {
        _id = id;
        _header = header;
    }

    /// <summary>
    /// Custom constructor used in <see cref="ConfigFactory.GetConfigGroup(ConfigPageModel, Core.Attributes.ConfigAttribute)"/> as a helper extension
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="id"></param>
    /// <param name="header"></param>
    internal ConfigCategory(ConfigPageModel parent, string id, string header)
    {
        _id = id;
        _header = header;
        parent.Categories.Add(this);
    }
}
