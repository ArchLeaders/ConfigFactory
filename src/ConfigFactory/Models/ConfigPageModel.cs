using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConfigFactory.Core;
using Dock.Model.Mvvm.Controls;
using System.Collections.ObjectModel;

namespace ConfigFactory.Models;

/// <summary>
/// An observable view model for the UI implemented ConfigPage control
/// </summary>
public partial class ConfigPageModel : Document
{
    public Dictionary<string, IConfigModule> ConfigModules { get; } = new();
    public Dictionary<string, ConfigItem> ItemsMap { get; } = new();

    /// <summary>
    /// The loaded <see cref="ConfigCategory"/> objects
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<ConfigCategory> _categories = new();

    /// <summary>
    /// The current selected <see cref="ConfigGroup"/>
    /// </summary>
    [ObservableProperty]
    private ConfigGroup? _selectedGroup;

    /// <summary>
    /// Gets or sets the secondary button Content property<br/>
    /// (Default: Save)
    /// </summary>
    [ObservableProperty]
    private object? _primaryButtonContent = "Save";

    /// <summary>
    /// Gets or sets the secondary button Content property<br/>
    /// (Default: Cancel)
    /// </summary>
    [ObservableProperty]
    private object? _secondaryButtonContent = "Cancel";

    /// <summary>
    /// Gets or sets the primary button IsEnabeld property<br/>
    /// (Default: True)
    /// </summary>
    [ObservableProperty]
    private bool _primaryButtonIsEnabled = true;

    /// <summary>
    /// Gets or sets the secondary button IsEnabeld property<br/>
    /// (Default: True)
    /// </summary>
    [ObservableProperty]
    private bool _secondaryButtonIsEnabled = true;

    /// <summary>
    /// The event the runs when the primary button is clicked
    /// </summary>
    public event Func<Task>? PrimaryButtonEvent;

    /// <summary>
    /// The event the runs when the secondary button is clicked
    /// </summary>
    public event Func<Task>? SecondaryButtonEvent;

    /// <summary>
    /// The event the runs <b><i>after</i></b> the primary button is clicked
    /// </summary>
    public event Func<Task>? PrimaryButtonCompletedEvent;

    /// <summary>
    /// The event the runs <b><i>after</i></b> the secondary button is clicked
    /// </summary>
    public event Func<Task>? SecondaryButtonCompletedEvent;

    /// <summary>
    /// The primary relay command used by the PrimaryButton control<br/>
    /// Executes the <see cref="PrimaryButtonEvent"/> and <see cref="PrimaryButtonCompletedEvent"/> events)
    /// </summary>
    /// <returns>The awaitable <see cref="Task"/></returns>
    [RelayCommand]
    public async Task PrimaryRelay()
    {
        await (PrimaryButtonEvent?.Invoke()
            ?? Task.CompletedTask);
        await (PrimaryButtonCompletedEvent?.Invoke()
            ?? Task.CompletedTask);
    }

    /// <summary>
    /// The primary relay command used by the PrimaryButton control<br/>
    /// Executes the <see cref="SecondaryButtonEvent"/> and <see cref="SecondaryButtonCompletedEvent"/> events)
    /// </summary>
    /// <returns>The awaitable <see cref="Task"/></returns>
    [RelayCommand]
    public async Task SecondaryRelay()
    {
        await (SecondaryButtonEvent?.Invoke()
            ?? Task.CompletedTask);
        await (SecondaryButtonCompletedEvent?.Invoke()
            ?? Task.CompletedTask);
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
