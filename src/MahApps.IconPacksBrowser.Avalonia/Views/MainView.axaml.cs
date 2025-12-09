using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MahApps.IconPacksBrowser.Avalonia.ViewModels;

namespace MahApps.IconPacksBrowser.Avalonia.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        _ = (DataContext as MainViewModel)!.LoadIconPacksAsync();
    }
}