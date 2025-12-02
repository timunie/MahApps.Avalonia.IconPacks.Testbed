using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.Input;
using MahApps.IconPacksBrowser.Avalonia.Helper;
using MahApps.IconPacksBrowser.Avalonia.Properties;

namespace MahApps.IconPacksBrowser.Avalonia.ViewModels;

public partial class SettingsViewModel
{

    public SettingsViewModel()
    {
        Settings.PropertyChanged += SettingsOnPropertyChanged;
    }

    private void SettingsOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {    
            case nameof(Settings.ExportTemplatesDir):
                CopyOriginalTemplatesCommand.NotifyCanExecuteChanged();
                ClearTemplatesDirCommand.NotifyCanExecuteChanged();
                break;
        }
    }

    /// <summary>
    /// Gets the current settings used
    /// </summary>
    public Settings Settings => Settings.Default;

    [RelayCommand]
    private void ToggleIsPreviewerVisible()
    {
        Settings.IsPreviewerVisible = !Settings.IsPreviewerVisible;
    }

    [RelayCommand]
    private async Task SelectTemplateFolderAsync()
    {
        try
        {
            var result = await this.OpenFolderDialogAsync(title: "Select the Template Folder");

            if (result?.SingleOrDefault() is { Length: > 0 } folder)
            {
                Settings.ExportTemplatesDir = folder;
            }
        }
        catch (Exception e)
        {
            await this.ShowMessageAsync("Error", e.Message);
        }
    }

    [RelayCommand]
    private async Task CopyOriginalTemplatesAsync()
    {
        try
        {
            var failedItems = new List<string>();
            var originalTemplates = AssetLoader.GetAssets(
                new Uri("ExportTemplates", UriKind.Relative),
                new Uri("avares://MahApps.IconPacksBrowser.Avalonia/Assets/", UriKind.Absolute));
            
                // Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "ExportTemplates"));

            foreach (var template in originalTemplates)
            {
                if (string.IsNullOrEmpty(Settings.ExportTemplatesDir)) 
                    throw new ArgumentException("ExportTemplatesDir is not specified");

                //Do your job with "file"  
                var destination = Path.Combine(Settings.ExportTemplatesDir, Path.GetFileName(template.LocalPath));
                if (!File.Exists(destination))
                {
                    await using (var fs = File.Create(destination))
                    {
                        await using (var assetStream = AssetLoader.Open(template))
                        {
                            assetStream.Seek(0, SeekOrigin.Begin);
                            await assetStream.CopyToAsync(fs);
                        }
                    }
                }
                else
                {
                    failedItems.Add("• " + Path.GetFileName(template.LocalPath));
                }
            }

            if (failedItems.Count > 0)
            {
                await this.ShowMessageAsync(
                    "Templates already exists",
                    $"The following files already exist in the templates folder. Either delete them or choose an empty folder. \n\n{string.Join(Environment.NewLine, failedItems)}"
                );
            }

            await this.OpenUriAsync(Settings.ExportTemplatesDir);
        }
        catch (Exception e)
        {
            await this.ShowMessageAsync("Error", e.Message);
        }
    }


    [RelayCommand]
    private void ClearTemplatesDir()
    {
        Settings.ExportTemplatesDir = null;
    }
}