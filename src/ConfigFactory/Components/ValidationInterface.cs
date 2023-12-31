﻿using ConfigFactory.Core.Attributes;
using ConfigFactory.Core.Components;
using ConfigFactory.Models;
using System.Reflection;

namespace ConfigFactory.Components;

internal class ValidationInterface : IValidationInterface
{
    private readonly ConfigPageModel _configPage;

    public ValidationInterface(ConfigPageModel configPageModel)
    {
        _configPage = configPageModel;
    }

    public void SetValidationColor(PropertyInfo property, string hexColorCode)
    {
        if (property.GetCustomAttribute<ConfigAttribute>() is ConfigAttribute attribute) {
            string key = $"{attribute.Category}/{attribute.Group}/{property.Name}";
            if (_configPage.ItemsMap.TryGetValue(key, out ConfigItem? configItem)) {
                configItem.ValidationColor = hexColorCode;
            }
        }
    }
}
