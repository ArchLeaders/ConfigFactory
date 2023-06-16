using CommunityToolkit.Mvvm.ComponentModel;
using ConfigFactory.Core;

namespace ConfigFactory.Avalonia.Demo.Models;

public partial class DemoConfig : ConfigModule<DemoConfig>
{
    [ObservableProperty]
    [property: Core.Attributes.Config(Header = "Some Field", Description = "Extended (probably redundant) description of Some Field, a text-based configuration")]
    private string _someField = string.Empty;
}
