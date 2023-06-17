using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ConfigFactory.Models;

public partial class ConfigGroup : ObservableObject
{
    [ObservableProperty]
    private string _header;

    [ObservableProperty]
    private bool _isSelected;

    [ObservableProperty]
    private ObservableCollection<ConfigItem> _items = new();

    public ConfigGroup(string header)
    {
        _header = header;
    }

    /// <summary>
    /// Custom constructor used in <see cref="ConfigFactory.GetConfigGroup(ConfigPageModel, Core.Attributes.ConfigAttribute)"/> as a helper extension
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="header"></param>
    internal ConfigGroup(ConfigCategory parent, string header)
    {
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
