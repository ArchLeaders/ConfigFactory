using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ConfigFactory.Avalonia.Models;

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
}
