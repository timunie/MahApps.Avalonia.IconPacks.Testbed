using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using MahApps.IconPacksBrowser.Avalonia.Helper;
using MahApps.IconPacksBrowser.Avalonia.Services;
using MahApps.IconPacksBrowser.Avalonia.ViewModels;

namespace MahApps.IconPacksBrowser.Avalonia.Properties;

public partial class Settings : ObservableObject
{
    public static Settings Default { get; private set; } = new Settings();

    /// <summary>
    /// Gets or sets the accent color of the App
    /// </summary>
    [ObservableProperty]
    [JsonConverter(typeof(JsonColorConverter))]
    public partial Color AccentColor { get; set; } = Color.Parse("#FF008A00");

    /// <summary>
    /// Gets or sets the requested AppTheme
    /// </summary>
    [ObservableProperty]
    public partial string AppTheme { get; set; } = "Default";

    /// <summary>
    /// Gets or sets the font size
    /// </summary>
    [ObservableProperty] 
    public partial double FontSize { get; set; } = 14;
    
    /// <summary>
    /// Gets or sets the folder with the export templates to use
    /// </summary>
    [ObservableProperty]
    public partial string? ExportTemplatesDir { get; set; }
    
    /// <summary>
    /// Gets or sets the preview background
    /// </summary>
    [ObservableProperty]
    [JsonConverter(typeof(JsonColorConverter))]
    public partial Color IconBackground { get; set; } = Colors.Transparent;

    /// <summary>
    /// Gets or sets the preview foreground
    /// </summary>
    [ObservableProperty]
    [JsonConverter(typeof(JsonColorConverter))]
    public partial Color IconForeground { get; set; } = Application.Current?.FindResource("ThemeAccentColor") as Color?
                                                        ?? Colors.Green;

    /// <summary>
    /// Gets or sets the preview size
    /// </summary>
    [ObservableProperty]
    public partial int IconPreviewSize { get; set; } = 48;

    partial void OnIconPreviewSizeChanged(int value)
    {
        if (value < 4) IconPreviewSize = 4;
    }

    /// <summary>
    /// Gets or sets the padding around the icon
    /// </summary>
    [ObservableProperty]
    public partial int IconPreviewPadding { get; set; } = 4;
    
    partial void OnIconPreviewPaddingChanged(int value)
    {
        if (value < 0) IconPreviewPadding = 0;
    }
    
    /// <summary>
    /// Gets or sets if the previewer is visible 
    /// </summary>
    [ObservableProperty]
    public partial bool IsPreviewerVisible { get; set; } = false;

    [ObservableProperty]
    public partial string[] FavoriteIconPacks { get; set; } = [];

    public void SaveSettings()
    {
        // Don't save during loading.'
        if (_isLoading) return;
        
        var json = JsonSerializer.Serialize(this, SettingsJsonContext.Default.Settings);
        try
        {
            var service = SettingsStorage.Get();
            service.WriteAsync(json);
        }
        catch
        {
            // ignore
        }
    }

    private static bool _isLoading; 
    
    public static async void LoadSettings()
    {
        try
        {
            _isLoading = true;
            
            var service = SettingsStorage.Get();
            var json = await service.ReadAsync();
            
            if (string.IsNullOrWhiteSpace(json))
                return;
            
            var settings = JsonSerializer.Deserialize(json, SettingsJsonContext.Default.Settings) ?? new Settings();
            
            Default.AccentColor = settings.AccentColor;
            Default.AppTheme = settings.AppTheme;
            Default.FontSize = settings.FontSize;
            Default.ExportTemplatesDir = settings.ExportTemplatesDir;
            Default.IconBackground = settings.IconBackground;
            Default.IconForeground = settings.IconForeground;
            Default.IconPreviewSize = settings.IconPreviewSize;
            Default.IconPreviewPadding = settings.IconPreviewPadding;
            Default.IsPreviewerVisible = settings.IsPreviewerVisible;
            Default.FavoriteIconPacks = settings.FavoriteIconPacks;

            // Reset colors if unable to read.
            if (Default.AccentColor.A < 255) Default.AccentColor = Color.Parse("#FF008A00");
            if (Default.IconForeground.A < 255) Default.IconForeground = Color.Parse("#FF008A00");
            
            MainViewModel.Instance.UpdateFavorites(settings.FavoriteIconPacks);
        }
        catch
        {
            // Empty 
        }
        finally
        {
            _isLoading = false;
        }
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        SaveSettings();
    }
}