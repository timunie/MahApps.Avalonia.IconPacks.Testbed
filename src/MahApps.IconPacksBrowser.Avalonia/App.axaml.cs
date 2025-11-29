using System.ComponentModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FluentAvalonia.Styling;
using MahApps.IconPacksBrowser.Avalonia.Properties;
using MahApps.IconPacksBrowser.Avalonia.ViewModels;
using MahApps.IconPacksBrowser.Avalonia.Views;

namespace MahApps.IconPacksBrowser.Avalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
        Settings.Default.PropertyChanged += SettingsOnPropertyChanged;
        Settings.LoadSettings();
    }

    private void SettingsOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case (nameof(Settings.AccentColor)):
                var fluentTheme = this.Styles.OfType<FluentAvaloniaTheme>().Single();
                fluentTheme.CustomAccentColor = Settings.Default.AccentColor;
                break;
        }
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = MainViewModel.Instance
            };
            
            desktop.ShutdownRequested +=  (_, _) => Settings.Default.SaveSettings();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = MainViewModel.Instance
            };
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}