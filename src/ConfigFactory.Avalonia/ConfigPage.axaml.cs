using Avalonia.Controls;
using ConfigFactory.Avalonia.Builders;

namespace ConfigFactory.Avalonia;
public partial class ConfigPage : UserControl
{
    static ConfigPage()
    {
        BooleanControlBuilder.Shared.Register();
        TextControlBuilder.Shared.Register();
        EnumControlBuilder.Shared.Register();
        NumericControlBuilder.Shared.Register();
    }

    public ConfigPage()
    {
        InitializeComponent();
        PART_FocusDelegate_1.PointerPressed += (s, e) => PART_FocusDelegate_1.Focus();
        PART_FocusDelegate_2.PointerPressed += (s, e) => PART_FocusDelegate_2.Focus();
    }
}
