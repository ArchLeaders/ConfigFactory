# Config Factory

[![License](https://img.shields.io/badge/License-AGPL%20v3.0-blue.svg)](License.md)

Generic library for creating extensible config arrangements to be used with a UI builder

## Table of Contents

- [Config Factory](#config-factory)
  - [Table of Contents](#table-of-contents)
- [Usage](#usage)
  - [How it works](#how-it-works)
  - [Creating a Config Module](#creating-a-config-module)
    - [Defining properties](#defining-properties)
  - [Building the Config Module for the Frontend](#building-the-config-module-for-the-frontend)
  - [Custom Control Builders](#custom-control-builders)
- [Avalonia Demo](#avalonia-demo)
- [Install](#install)
  - [NuGet](#nuget)
  - [Git Submodule](#git-submodule)

---

# Usage

ConfigFactory is designed to be a modular configuration manager that can be paired with a UI builder to construct a user interface to edit the properties. This allows for a clean configuration setup without manually creating a UI page for each property in your config module (settings model).

## How it works

There are two main libraries required to use ConfigFactory, the core library and the UI framework-specific library.

The core library is a lightweight library with the vanilla [configuration attributes](...) and [IConfigModule](...) interface. The `IConfigModule` is the signature required by the front end to build a user interface. The library also comes with a `ConfigModule<T>` helper type which implements the `IConfigModule` interface using a simple json IO method to read and write the config module.

The front-end library contains code specific to a UI framework to generate the user interface. These libraries will use their documentation for specific usage, in most cases, however, it is simply a matter of using their `ConfigPage` view in your application.

There is also the option to create a custom frontend by implementing a view for the `ConfigPageModel` (see [...](...)).

## Creating a Config Module

First and foremost, add the `ConfigFactory.Core` library to your backend project library.

Then, create a new partial class and implement the `IConfigModule` or `ConfigModule<T>` where `T` is the class (recommended):

```cs
public partial class SomeConfig : ConfigModule<SomeConfig>
{

}
```

### Defining properties

There are of course many ways to define the properties, but by far the easiest and safest way is by using the `CommunityToolkit.Mvvm` `ObservableProperty` attribute to source-generate the `INotifyOfPropertyChanged` update methods.

```cs
[ObservableProperty]
private string _someProperty = string.Empty;
```

This will generate the observable `SomeProperty` when the library compiles. Learn more about the `ObservableProperty` attribute on the Microsoft [docs](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/generators/observableproperty).

The next step is to add the `Config` attribute to the property, this can be done using the `property` keyword.

```cs
[ObservableProperty]
[property: Config(
    Header = "Some property",
    Description = "Extended description of some property")]
private string _someProperty = string.Empty;
```

You can also customize the `Category` *(defaults to `General`)* and `Group` *(defaults to `Common`)* of the property as well.

```cs
[ObservableProperty]
[property: Config(
    Header = "Some property",
    Description = "Extended description of some property",
    Category = "Some category",
    Group = "Some group")]
private string _someProperty = string.Empty;
```

You should be left with a class similar to the following:

```cs
public partial class SomeConfig : ConfigModule<SomeConfig>
{
    [ObservableProperty]
    [property: Config(
        Header = "Some property",
        Description = "Extended description of some property",
        Category = "Some category",
        Group = "Some group")]
    private string _someProperty = string.Empty;
}
```

#### Property Validation

Along with creating properties, you can also set custom validations for them.

When using the ObservableProperty attribute, implement the partial `OnPropertyNameChanged` function to register a custom validation.

```cs
partial void OnSomePropertyChanged(string value)
{
    SetValidation(() => SomeProperty, value => {
        return value is "Some Proper Value";
    });
}
```

\**(In order to achieve live validation, this function must be called in the OnChanged function/event of the property.)*

## Building the Config Module for the Frontend

Building a config module is a simple as calling the static function `ConfigFactory.Build<T>()` where `T` implements `ConfigModule<T>`, or `ConfigFactory.Build(IConfigModule module)` with other `IConfigModule` implementations.

This will return a `ConfigPageModel` which is used as the `DataContext` for any UI framework-specific library.

Alternatively, if the `ConfigPageModel`/`DataContext` is already constructed by the view, you can use the extension function `ConfigPageModel.Append<T>()` to inject your `ConfigModule` into the `ConfigPageModel` at runtime.

## Custom Control Builders

Custom control builders allow you to inject a control stack specific to your own property types.

For example, if you have a custom record type that contains two text values, you can create a custom control builder to render two text boxes on the setting page.

```cs
public partial class SomeObject : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;
}
```

```cs
public partial class SomeConfig : ConfigModule<SomeConfig>
{
    [ObservableProperty]
    [property: Config(
        Header = "Some property",
        Description = "Extended description of some property",
        Category = "Some category",
        Group = "Some group")]
    private SomeObject _someProperty = new(string.Empty, string.Empty);
}
```

```cs
public class SomeObjectControlBuilder : ControlBuilder<SomeObjectControlBuilder>
{
    public override object? Build(IConfigModule context, PropertyInfo propertyInfo)
    {
        return new StackPanel {
            DataContext = context,
            Orientation = Orientation.Horizontal,
            Spacing = 5,
            Children = {
                new TextBox {
                    VerticalAlignment = VerticalAlignment.Top,
                    [!TextBox.TextProperty] = new Binding(propertyInfo.Name + ".Name")
                },
                new TextBox {
                    VerticalAlignment = VerticalAlignment.Top,
                    [!TextBox.TextProperty] = new Binding(propertyInfo.Name + ".Description")
                }
            }
        };
    }

    public override bool IsValid(object? value)
    {
        return value is SomeObject;
    }
}
```

*(The last code snippet is specific to the Avalonia UI framework)*

# Add Avalonia Style

Add the style to app.axaml.

```
    <Application.Styles>
        <FluentTheme />
        <StyleInclude Source="avares://ConfigFactory.Avalonia/Themes/Fluent/ConfigFactory.Avalonia.axaml" />
    </Application.Styles>
```
# Avalonia Demo 

Check out the [demo application](https://github.com/ArchLeaders/ConfigFactory/tree/master/src/ConfigFactory.Avalonia.Demo) for a complete implementation.

# Install

Install with NuGet or add the source code as a git submodule.

## NuGet

```powershell
# ViewModel interface package (custom UI implementation)
Install-Package ConfigFactory

# Interface package (for project library)
Install-Package ConfigFactory.Core

# Avalonia UI package
Install-Package ConfigFactory.Avalonia
```

## Git Submodule

```powershell
git submodule add "https://github.com/ArchLeaders/ConfigFactory.git" "lib/ConfigFactory"
```

---

**© 2023 Arch Leaders**
