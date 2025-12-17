using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace MahApps.IconPacksBrowser.Avalonia.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Update caption buttons placeholder width when window opens and on size changes.
        this.Opened += (_, _) => UpdateCaptionButtonsWidth();
        this.GetObservable(ClientSizeProperty).Subscribe(_ => UpdateCaptionButtonsWidth());
    }

    private void UpdateCaptionButtonsWidth()
    {
        try
        {
            // Try to use Avalonia 11 TitleBar API if available via reflection to avoid hard dependency
            var titleBarProp = GetType().GetProperty("TitleBar");
            if (titleBarProp != null)
            {
                var titleBar = titleBarProp.GetValue(this);
                if (titleBar != null)
                {
                    var rightInsetProp = titleBar.GetType().GetProperty("SystemOverlayRightInset");
                    if (rightInsetProp != null)
                    {
                        var insetObj = rightInsetProp.GetValue(titleBar);
                        if (insetObj is double inset && inset > 0)
                        {
                            Resources["SystemCaptionButtonsWidth"] = inset;
                            return;
                        }
                    }
                }
            }
        }
        catch
        {
            // ignore and use fallback below
        }

        // Fallback value if platform inset is not available.
        // 140 is a reasonable default for Win/Linux/macOS at 100% scale.
        Resources["SystemCaptionButtonsWidth"] = 140d;
    }
}