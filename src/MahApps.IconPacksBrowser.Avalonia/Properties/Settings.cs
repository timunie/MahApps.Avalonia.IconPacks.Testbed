using System;
using System.IO;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using MahApps.IconPacksBrowser.Avalonia.Helper;

namespace MahApps.IconPacksBrowser.Avalonia.Properties;

public partial class Settings : ObservableObject
{
    public static Settings Default { get; private set; } = new Settings();

    /// <summary>
    /// Gets or sets the accent color of the App
    /// </summary>
    [ObservableProperty]
    public partial Color AccentColor { get; set; } = Color.Parse("#FF008A00");

    /// <summary>
    /// Gets or sets the requested AppTheme
    /// </summary>
    [ObservableProperty]
    public partial string AppTheme { get; set; } = "Default";

    /// <summary>
    /// Gets or sets the folder with the export templates to use
    /// </summary>
    [ObservableProperty]
    public partial string? ExportTemplatesDir { get; set; }

    /// <summary>
    /// Gets or sets the preview background
    /// </summary>
    [ObservableProperty]
    public partial Color IconBackground { get; set; } = Colors.Transparent;

    /// <summary>
    /// Gets or sets the preview foreground
    /// </summary>
    [ObservableProperty]
    public partial Color IconForeground { get; set; } = Application.Current?.FindResource("SystemAccentColor") as Color?
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
    

    public void SaveSettings()
    {
        var settingsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MahApps.IconPacksBrowser");
        var settingsFile = Path.Combine(settingsDir, "Settings.json");

        if (!Directory.Exists(settingsDir))
        {
            Directory.CreateDirectory(settingsDir);
        }
        
        var json = JsonSerializer.Serialize(this, SettingsJsonContext.Default.Settings);
        File.WriteAllText(settingsFile, json);
    }

    public static void LoadSettings()
    {
        try
        {
            var settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MahApps.IconPacksBrowser", "Settings.json");
            var json = File.ReadAllText(settingsFile);
            var settings = JsonSerializer.Deserialize(json, SettingsJsonContext.Default.Settings) ?? new Settings();
            
            Default.AccentColor = settings.AccentColor;
            Default.AppTheme = settings.AppTheme;
            Default.ExportTemplatesDir = settings.ExportTemplatesDir;
            Default.IconBackground = settings.IconBackground;
            Default.IconForeground = settings.IconForeground;
            Default.IconPreviewSize = settings.IconPreviewSize;
            Default.IconPreviewPadding = settings.IconPreviewPadding;
            Default.IsPreviewerVisible = settings.IsPreviewerVisible;
        }
        catch
        {
            // Empty 
        }
    }
}