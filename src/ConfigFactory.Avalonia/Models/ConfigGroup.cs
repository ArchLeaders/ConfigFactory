using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConfigFactory.Avalonia.Controls;
using System.Collections.ObjectModel;

namespace ConfigFactory.Avalonia.Models;

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

    [RelayCommand]
    public Task SelectGroupRelay(ConfigPageModel page)
    {
        IsSelected = true;
        page.SelectedGroup = this;
        return Task.CompletedTask;
    }
}
