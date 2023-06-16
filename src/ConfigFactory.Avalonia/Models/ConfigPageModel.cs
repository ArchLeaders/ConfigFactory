using Avalonia.Markup.Xaml.MarkupExtensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConfigFactory.Avalonia.Controls;
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

    [ObservableProperty]
    private Func<Task>? _primaryButtonAction;

    [ObservableProperty]
    private Func<Task>? _secondaryButtonAction;

    [RelayCommand]
    public Task PrimaryRelay()
    {
        return PrimaryButtonAction?.Invoke()
            ?? Task.CompletedTask;
    }

    [RelayCommand]
    public Task SecondaryRelay()
    {
        return SecondaryButtonAction?.Invoke()
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

#if DEBUG
    public ConfigPageModel()
    {
        Categories.Add(new("Category 1") {
            Groups = {
                new ConfigGroup("Group 1") {
                    Items = {
                        new ConfigItem {
                            Header = "Group 1, Item 1",
                            Description = "Group 1, Item 1 Description"
                        },
                        new ConfigItem {
                            Header = "Group 1, Item 2",
                            Description = "Group 1, Item 2 Description"
                        },
                        new ConfigItem {
                            Header = "Group 1, Item 3",
                            Description = "Group 1, Item 3 Description"
                        }
                    }
                },
                new ConfigGroup("Group 2") {
                    Items = {
                        new ConfigItem {
                            Header = "Group 2, Item 1",
                            Description = "Group 2, Item 1 Description"
                        },
                        new ConfigItem {
                            Header = "Group 2, Item 2",
                            Description = "Group 2, Item 2 Description"
                        },
                        new ConfigItem {
                            Header = "Group 2, Item 3",
                            Description = "Group 2, Item 3 Description"
                        }
                    }
                }
            }
        });

        Categories.Add(new("Category 2") {
            Groups = {
                new ConfigGroup("Group 1") {
                    Items = {
                        new ConfigItem {
                            Header = "Group 1, Item 1",
                            Description = "Group 1, Item 1 Description"
                        },
                        new ConfigItem {
                            Header = "Group 1, Item 2",
                            Description = "Group 1, Item 2 Description"
                        },
                        new ConfigItem {
                            Header = "Group 1, Item 3",
                            Description = "Group 1, Item 3 Description"
                        }
                    }
                },
                new ConfigGroup("Group 2") {
                    Items = {
                        new ConfigItem {
                            Header = "Group 2, Item 1",
                            Description = "Group 2, Item 1 Description"
                        },
                        new ConfigItem {
                            Header = "Group 2, Item 2",
                            Description = "Group 2, Item 2 Description"
                        },
                        new ConfigItem {
                            Header = "Group 2, Item 3",
                            Description = "Group 2, Item 3 Description"
                        }
                    }
                }
            }
        });

        Categories.Add(new("Category 3") {
            Groups = {
                new ConfigGroup("Group 1") {
                    Items = {
                        new ConfigItem {
                            Header = "Group 1, Item 1",
                            Description = "Group 1, Item 1 Description"
                        },
                        new ConfigItem {
                            Header = "Group 1, Item 2",
                            Description = "Group 1, Item 2 Description"
                        },
                        new ConfigItem {
                            Header = "Group 1, Item 3",
                            Description = "Group 1, Item 3 Description"
                        }
                    }
                },
                new ConfigGroup("Group 2") {
                    Items = {
                        new ConfigItem {
                            Header = "Group 2, Item 1",
                            Description = "Group 2, Item 1 Description"
                        },
                        new ConfigItem {
                            Header = "Group 2, Item 2",
                            Description = "Group 2, Item 2 Description"
                        },
                        new ConfigItem {
                            Header = "Group 2, Item 3",
                            Description = "Group 2, Item 3 Description"
                        }
                    }
                }
            }
        });

        SelectedGroup = Categories[0].Groups[0];
    }
#endif
}
