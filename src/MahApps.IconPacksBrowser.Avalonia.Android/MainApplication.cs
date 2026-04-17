using System;
using Android.App;
using Android.Runtime;
using Avalonia;
using Avalonia.Android;
using MahApps.IconPacksBrowser.Avalonia;
using MahApps.IconPacksBrowser.Avalonia.Android.Services;
using MahApps.IconPacksBrowser.Avalonia.Services;

namespace AdvancedToDoList.Android;

[Application]
public class MainApplication : AvaloniaAndroidApplication<App>
{
    protected MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
        : base(javaReference, transfer)
    {
    }

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        // Register Android-specific services before App initialization.

        return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .AfterSetup(_ =>
            {
                // Register Android settings storage service
                SettingsStorage.Register(new AndroidSettingsStorageService());
            });
    }
}

