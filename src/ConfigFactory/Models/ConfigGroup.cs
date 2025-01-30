using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ConfigFactory.Core;
using ConfigFactory.Core.Attributes;

namespace ConfigFactory.Models;

public partial class ConfigGroup : ObservableObject
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
    private bool _isSelected;

    [ObservableProperty]
    private ObservableCollection<ConfigItem> _items = new();

    public ConfigGroup(string id, string header)
    {
        _id = id;
        _header = header;
    }

    /// <summary>
    /// Custom constructor used in <see cref="ConfigFactory.GetConfigGroup(ConfigPageModel,IConfigModule,ConfigAttribute)"/> as a helper extension
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="id"></param>
    /// <param name="header"></param>
    internal ConfigGroup(ConfigCategory parent, string id, string header)
    {
        _id = id;
        _header = header;
        parent.Groups.Add(this);
    }

    [RelayCommand]
    public Task SelectGroupRelay(ConfigPageModel page)
    {
        IsSelected = true;
        page.SelectedGroup = this;
        return Task.CompletedTask;
    }
}
