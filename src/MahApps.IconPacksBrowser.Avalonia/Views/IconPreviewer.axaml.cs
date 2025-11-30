using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using MahApps.IconPacksBrowser.Avalonia.Properties;

namespace MahApps.IconPacksBrowser.Avalonia.Views;

public partial class IconPreviewer : UserControl
{
    public IconPreviewer()
    {
        InitializeComponent();
        PreviewScrollViewer.AddHandler(PointerWheelChangedEvent, PreviewScrollViewer_OnPointerWheelChanged, RoutingStrategies.Tunnel);
    }

    private void PreviewScrollViewer_OnPointerWheelChanged(object? sender, PointerWheelEventArgs e)
    {
        if (e.KeyModifiers == KeyModifiers.Control)
        {
            Settings.Default.IconPreviewSize += (int)(e.Delta.Y * 5);
            e.Handled = true;
        }
    }
}