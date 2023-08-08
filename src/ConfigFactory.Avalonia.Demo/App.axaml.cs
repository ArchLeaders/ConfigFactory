using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ConfigFactory.Avalonia.Demo.ViewModels;
using ConfigFactory.Avalonia.Demo.Views;
using ConfigFactory.Avalonia.Helpers;
using System.Globalization;

namespace ConfigFactory.Avalonia.Demo;
public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            // Change locale here (e.g. th-TH)
            Assets.Translations.Culture = new CultureInfo("en-US");

            desktop.MainWindow = new MainWindow {
                DataContext = new MainWindowViewModel(),
            };

            BrowserDialog.StorageProvider = desktop.MainWindow.StorageProvider;
        }

        base.OnFrameworkInitializationCompleted();
    }
}