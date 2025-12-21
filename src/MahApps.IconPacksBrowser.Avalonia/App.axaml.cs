using System.ComponentModel;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;
using IconPacks.Avalonia.Core;
using MahApps.IconPacksBrowser.Avalonia.Helper;
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

        // Increase cache size, since we expect a lot of icons
        PackIconGeometryCache.CacheSize = 1500;
        
        // initial accent color
        ApplyAccentColor(Settings.Default.AccentColor);
    }

    private void SettingsOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case (nameof(Settings.AccentColor)):
            case (nameof(Settings.AppTheme)):
                ApplyAccentColor(Settings.Default.AccentColor);
                break;
            case (nameof(Settings.FontSize)):
                Resources["FontSizeNormal"] = Settings.Default.FontSize;
                Resources["FontSizeLarge"] = Settings.Default.FontSize * 1.25;
                Resources["FontSizeSmall"] = Settings.Default.FontSize * 0.85;
                break;
        }
    }

    private void ApplyAccentColor(Color accent)
    {
        var isDarkMode = ActualThemeVariant == ThemeVariant.Dark;
        var accent2 = accent.AdjustAccent(0.2, isDarkMode);
        var accent3 = accent.AdjustAccent(0.4, isDarkMode);
        var accent4 = accent.AdjustAccent(0.6, isDarkMode);

        Resources["ThemeAccentColor"] = accent;
        Resources["ThemeAccentColor2"] = accent2;
        Resources["ThemeAccentColor3"] = accent3;
        Resources["ThemeAccentColor4"] = accent4;
        
        Resources["ThemeAccentBrush"] = new SolidColorBrush(accent);
        Resources["ThemeAccentBrush2"] = new SolidColorBrush(accent2);
        Resources["ThemeAccentBrush3"] = new SolidColorBrush(accent3);
        Resources["ThemeAccentBrush4"] = new SolidColorBrush(accent4);
        
        Resources["HighlightForegroundColor"] = ColorHelper.GetIdealForeground(accent);
        Resources["HighlightForegroundBrush"] = new SolidColorBrush(ColorHelper.GetIdealForeground(accent));
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = MainViewModel.Instance
            };
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