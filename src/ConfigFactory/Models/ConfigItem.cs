﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConfigFactory.Models;

public partial class ConfigItem : ObservableValidator
{
    [ObservableProperty]
    [Required]
    private string _header = string.Empty;

    [ObservableProperty]
    [Required]
    private string _description = string.Empty;

    [ObservableProperty]
    private string _validationColor = "#00FFFFFF";

    [ObservableProperty]
    [Required]
    private object? _content;
}
