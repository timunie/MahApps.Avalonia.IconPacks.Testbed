using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input.Platform;
using Avalonia.Platform.Storage;
using MahApps.IconPacksBrowser.Avalonia.Controls.Dialogs;

namespace MahApps.IconPacksBrowser.Avalonia.Helper;

public class DialogManager
{
    private static readonly Dictionary<object, Visual> _RegistrationMapper =
        new Dictionary<object, Visual>();

    static DialogManager()
    {
        RegisterProperty.Changed.AddClassHandler<Visual>(RegisterChanged);
    }

    private static void RegisterChanged(Visual sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (sender is null)
        {
            throw new InvalidOperationException("The DialogManager can only be registered on a Visual");
        }

        // Unregister any old registered context
        if (e.OldValue != null)
        {
            _RegistrationMapper.Remove(e.OldValue);
        }

        // Register any new context
        if (e.NewValue != null)
        {
            var newValue = e.NewValue; 
            _RegistrationMapper[newValue] = sender;
            
            // Remove the registration when the visual is detached from the visual tree
            sender.DetachedFromVisualTree += (_, _) =>
            {
                if (_RegistrationMapper.TryGetValue(newValue, out var current) && ReferenceEquals(current, sender))
                {
                    _RegistrationMapper.Remove(newValue);
                }
            };
        }
    }

    /// <summary>
    /// This property handles the registration of Views and ViewModel
    /// </summary>
    public static readonly AttachedProperty<object?> RegisterProperty = AvaloniaProperty.RegisterAttached<DialogManager, Visual, object?>(
        "Register");

    /// <summary>
    /// Accessor for Attached property <see cref="RegisterProperty"/>.
    /// </summary>
    public static void SetRegister(AvaloniaObject element, object value)
    {
        element.SetValue(RegisterProperty, value);
    }

    /// <summary>
    /// Accessor for Attached property <see cref="RegisterProperty"/>.
    /// </summary>
    public static object? GetRegister(AvaloniaObject element)
    {
        return element.GetValue(RegisterProperty);
    }

    /// <summary>
    /// Gets the associated <see cref="Visual"/> for a given context. Returns null, if none was registered
    /// </summary>
    /// <param name="context">The context to lookup</param>
    /// <returns>The registered Visual for the context or null if none was found</returns>
    public static Visual? GetVisualForContext(object context)
    {
        return _RegistrationMapper.TryGetValue(context, out var result) ? result : null;
    }

    /// <summary>
    /// Gets the parent <see cref="TopLevel"/> for the given context. Returns null, if no TopLevel was found
    /// </summary>
    /// <param name="context">The context to lookup</param>
    /// <returns>The registered TopLevel for the context or null if none was found</returns>
    public static TopLevel? GetTopLevelForContext(object context)
    {
        return TopLevel.GetTopLevel(GetVisualForContext(context));
    }
}

/// <summary>
/// A helper class to manage dialogs via extension methods. Add more on your own
/// </summary>
public static class DialogHelper
{
    /// <summary>
    /// Shows an open file dialog for a registered context, most likely a ViewModel
    /// </summary>
    /// <param name="context">The context</param>
    /// <param name="title">The dialog title or a default is null</param>
    /// <param name="selectMany">Is selecting many files allowed?</param>
    /// <returns>An array of file names</returns>
    /// <exception cref="ArgumentNullException">if context was null</exception>
    public static async Task<IEnumerable<string>?> OpenFileDialogAsync(this object? context, string? title = null, bool selectMany = true)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // lookup the TopLevel for the context
        var topLevel = DialogManager.GetTopLevelForContext(context);

        if (topLevel != null)
        {
            // Open the file dialog
            var storageFiles = await topLevel.StorageProvider.OpenFilePickerAsync(
                new FilePickerOpenOptions()
                {
                    AllowMultiple = selectMany,
                    Title = title ?? "Select any file(s)"
                });

            // return the result
            return storageFiles.Select(s => s.TryGetLocalPath() ?? s.Name);
        }

        return null;
    }

    /// <summary>
    /// Shows an open folder dialog for a registered context, most likely a ViewModel
    /// </summary>
    /// <param name="context">The context</param>
    /// <param name="title">The dialog title or a default is null</param>
    /// /// <param name="selectMany">Is selecting many folders allowed?</param>
    /// <returns>An array of folder names</returns>
    /// <exception cref="ArgumentNullException">if context was null</exception>
    public static async Task<IEnumerable<string>?> OpenFolderDialogAsync(this object? context, string? title = null, bool selectMany = true)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // lookup the TopLevel for the context
        var topLevel = DialogManager.GetTopLevelForContext(context);

        if (topLevel != null)
        {
            // Open the file dialog
            var storageFolders = await topLevel.StorageProvider.OpenFolderPickerAsync(
                new FolderPickerOpenOptions()
                {
                    AllowMultiple = selectMany,
                    Title = title ?? "Select any folder(s)"
                });

            // return the result
            return storageFolders.Select(s => s.TryGetLocalPath() ?? s.Name);
        }

        return null;
    }

    /// <summary>
    /// Shows a save file dialog for a registered context, most likely a ViewModel
    /// </summary>
    /// <param name="context">The context</param>
    /// <param name="title">The dialog title or a default is null</param>
    /// <param name="filters">The filter to use</param>
    /// <param name="fileNameSuggestion">the suggested file name</param>
    /// <param name="defaultExtension">the default extension</param>
    /// <returns>The chosen file as storage item</returns>
    /// <exception cref="ArgumentNullException">if context was null</exception>
    public static async Task<IStorageFile?> SaveFileDialogAsync(this object? context, string? title = null, IReadOnlyList<FilePickerFileType>? filters = null,
        string? fileNameSuggestion = null, string? defaultExtension = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // lookup the TopLevel for the context
        var topLevel = DialogManager.GetTopLevelForContext(context);

        if (topLevel != null)
        {
            // Open the file dialog
            var storageFile = await topLevel.StorageProvider.SaveFilePickerAsync(
                new FilePickerSaveOptions()
                {
                    ShowOverwritePrompt = true,
                    FileTypeChoices = filters,
                    Title = title ?? "Save File As", 
                    SuggestedFileName = fileNameSuggestion
                });

            // return the result
            if (storageFile != null) return storageFile;
        }

        return null;
    }
    
    public static async Task ShowMessageAsync(this object? context, string? title, object? content)
    {
        ArgumentNullException.ThrowIfNull(context);
        
        await ShowOverlayDialogAsync<DialogResult?>(context, title ?? string.Empty, content, DialogCommands.Ok);
    }

    public static async Task SetClipboardContentAsync(this object? context, string content)
    {
        ArgumentNullException.ThrowIfNull(context);

        // lookup the TopLevel for the context
        var topLevel = DialogManager.GetTopLevelForContext(context);

        if (topLevel?.Clipboard != null)
        {
            await topLevel.Clipboard.SetTextAsync(content);
        }
    }

    public static async Task OpenUriAsync(this object? context, object? uri)
    {
        ArgumentNullException.ThrowIfNull(context);

        var topLevel = DialogManager.GetTopLevelForContext(context);
        Uri? navigateUri = uri switch
        {
            Uri u => u,
            string s => new Uri(s),
            _ => null
        };

        if (navigateUri is not null)
        {
            await topLevel!.Launcher.LaunchUriAsync(navigateUri);
        }
    }
    
    /// <summary>
    /// Shows an overlay dialog for a given context.
    /// </summary>
    /// <param name="context">the context to resolve the TopLevel</param>
    /// <param name="title">The title of the dialog</param>
    /// <param name="content">The content to show</param>
    /// <param name="dialogCommands">The <see cref="DialogCommands"/> to show built in commands</param>
    /// <typeparam name="T">The expected return type</typeparam>
    /// <returns>the dialog result</returns>
    /// <exception cref="InvalidOperationException">If either the Toplevel or the Overlayer wasn't found</exception>
    public static async Task<T?> ShowOverlayDialogAsync<T>(
        this object? context, 
        string title,
        object? content,
        params DialogCommand[] dialogCommands)
    {
        var tcs = new TaskCompletionSource<object?>();

        ArgumentNullException.ThrowIfNull(context);

        var overlayDialog = new OverlayDialog(tcs)
        {
            Content = content,
            Header = title,
            DialogCommands = dialogCommands
        };

        // Get the owner window. If it is null, throw an exception
        var topLevel = DialogManager.GetTopLevelForContext(context)
                       ?? throw new InvalidOperationException("Unable to find TopLevel for context");
        
        var overlayLayer = OverlayLayer.GetOverlayLayer(topLevel) ??
                           throw new InvalidOperationException("Unable to find OverlayLayer");
        
        overlayLayer.Children.Add(overlayDialog); 

        var result = await tcs.Task;
        
        overlayLayer.Children.Remove(overlayDialog);
        
        return (T?)result;
    }
}