using Avalonia.Controls;

namespace ConfigFactory.Avalonia.Controls;
public partial class ConfigPage : UserControl
{
    public ConfigPage()
    {
        InitializeComponent();
        PART_FocusDelegate_1.PointerPressed += (s, e) => PART_FocusDelegate_1.Focus();
        PART_FocusDelegate_2.PointerPressed += (s, e) => PART_FocusDelegate_2.Focus();
    }
}
