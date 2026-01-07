using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace MahApps.IconPacksBrowser.Avalonia.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        
        CollapsePaneIfWidthTooSmall();
    }

    protected override void OnSizeChanged(SizeChangedEventArgs e)
    {
        base.OnSizeChanged(e);
        
        NavigationView.DisplayMode = e.NewSize.Width < 800 
            ? SplitViewDisplayMode.Overlay : SplitViewDisplayMode.Inline;
        
        CollapsePaneIfWidthTooSmall();
    }
    
    private void CollapsePaneIfWidthTooSmall()
    {
        if (IsLoaded && this.Bounds.Width < 800)
            NavigationView.IsPaneOpen = false;   
    }
    
    
    private void Navigation_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        // Only collapse the pane if the left mouse button was pressed
        if (e.InitialPressMouseButton != MouseButton.Left) 
            return;
        CollapsePaneIfWidthTooSmall();
    }
}