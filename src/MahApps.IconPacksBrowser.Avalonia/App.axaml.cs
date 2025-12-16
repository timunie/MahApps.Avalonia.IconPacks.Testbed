using System.ComponentModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Themes.Simple;
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
        }
    }

    private void ApplyAccentColor(Color accent)
    {
        var accentHsl = accent.ToHsl();
        Color accent2;
        Color accent3;

        if (ActualThemeVariant == ThemeVariant.Light)
        {
            accent2 = HslColor.FromHsl(accentHsl.H, accentHsl.S, accentHsl.L * 0.8).ToRgb();
            accent3 = HslColor.FromHsl(accentHsl.H, accentHsl.S, accentHsl.L * 0.6).ToRgb();
        }
        else
        {
            static double Clamp01(double v) => v < 0 ? 0 : (v > 1 ? 1 : v);
            static double Brighten(double l, double amount) => Clamp01(l + (1.0 - l) * amount);

            accent2 = HslColor.FromHsl(accentHsl.H, accentHsl.S, Brighten(accentHsl.L, 0.2)).ToRgb();
            accent3 = HslColor.FromHsl(accentHsl.H, accentHsl.S, Brighten(accentHsl.L, 0.4)).ToRgb(); 
        }
        
        Resources["ThemeAccentColor"] = accent;
        Resources["ThemeAccentColor2"] = accent2;
        Resources["ThemeAccentColor3"] = accent3;
        
        Resources["ThemeAccentBrush"] = new SolidColorBrush(accent);
        Resources["ThemeAccentBrush2"] = new SolidColorBrush(accent2);
        Resources["ThemeAccentBrush3"] = new SolidColorBrush(accent3);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = MainViewModel.Instance
            };

            desktop.ShutdownRequested += (_, _) => Settings.Default.SaveSettings();
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