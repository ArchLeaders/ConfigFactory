using Avalonia.Controls;
using ConfigFactory.Avalonia.Demo.Models;
using ConfigFactory.Models;

namespace ConfigFactory.Avalonia.Demo.Views;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        if (ConfigPage.DataContext is ConfigPageModel model) {
            model.Append<DemoConfig>();
        }
    }
}