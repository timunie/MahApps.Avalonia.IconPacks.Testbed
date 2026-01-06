using IconPacks.Avalonia.MaterialDesign;
using System;
using IconPacks.Avalonia.Octicons;
using IconPacks.Avalonia.PhosphorIcons;

namespace MahApps.IconPacksBrowser.Avalonia.ViewModels;

public partial class NavigationItemViewModelBase : ViewModelBase
{
    public object? Tag { get; init; }

    public string? Title { get; init; }
    
    public Enum? Icon { get; init; }
}

public class SeparatorNavigationItemViewModel : NavigationItemViewModelBase;

public class IconPackNavigationItemViewModel : NavigationItemViewModelBase
{
    public IconPackNavigationItemViewModel(IconPackViewModel iconPack)
    {
        Title = iconPack.Caption;
        Tag = iconPack;
        IconPack = iconPack;
        Icon = PackIconOcticonsKind.StarFill;
    }

    public IconPackViewModel IconPack { get; }
    
    public override string ToString() => Title ?? "Unknown IconPack";
}

public class WelcomeNavigationItem : NavigationItemViewModelBase
{
    public WelcomeNavigationItem()
    {
        Title = "Welcome";
        Icon = PackIconMaterialDesignKind.HomeOutline;
    }
    
    public override string ToString() => "Welcome";
}

public class AllIconPacksNavigationItemViewModel : NavigationItemViewModelBase
{
    public AllIconPacksNavigationItemViewModel()
    {
        Title = "All Icons";
        Icon = PackIconPhosphorIconsKind.Shapes;
    }

    public MainViewModel MainViewModel => MainViewModel.Instance;
    
    public override string ToString() => "All Icons";
}

public class SettingsNavigationItem : NavigationItemViewModelBase
{
    public SettingsNavigationItem()
    {
        Title = "Settings";
        Icon = PackIconMaterialDesignKind.Settings;
        Tag = new SettingsViewModel();
    }
    
    public override string ToString() => "Settings";
}

public class AboutNavigationItem : NavigationItemViewModelBase
{
    public AboutNavigationItem()
    {
        Title = "About";
        Icon = PackIconMaterialDesignKind.InfoOutline;
        Tag = new AboutViewModel();
    }

    public override string ToString() => "About";
}