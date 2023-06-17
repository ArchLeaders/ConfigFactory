using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ConfigFactory.Avalonia.Models;

public partial class ConfigPageModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<ConfigCategory> _categories = new();

    [ObservableProperty]
    private ConfigGroup? _selectedGroup;

    [ObservableProperty]
    private object? _primaryButtonContent = "Save";

    [ObservableProperty]
    private object? _secondaryButtonContent = "Cancel";

    [ObservableProperty]
    private bool _primaryButtonIsEnabled = true;

    [ObservableProperty]
    private bool _secondaryButtonIsEnabled = true;

    public event Func<Task>? PrimaryButtonEvent;
    public event Func<Task>? SecondaryButtonEvent;

    [RelayCommand]
    public Task PrimaryRelay()
    {
        return PrimaryButtonEvent?.Invoke()
            ?? Task.CompletedTask;
    }

    [RelayCommand]
    public Task SecondaryRelay()
    {
        return SecondaryButtonEvent?.Invoke()
            ?? Task.CompletedTask;
    }

    partial void OnSelectedGroupChanged(ConfigGroup? oldValue, ConfigGroup? newValue)
    {
        if (oldValue is ConfigGroup oldGroup) {
            oldGroup.IsSelected = false;
        }

        if (newValue is ConfigGroup newGroup) {
            newGroup.IsSelected = true;
        }
    }
}
