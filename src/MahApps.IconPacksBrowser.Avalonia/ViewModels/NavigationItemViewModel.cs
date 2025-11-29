using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;

namespace MahApps.IconPacksBrowser.Avalonia.ViewModels;

public partial class NavigationItemViewModelBase : ViewModelBase
{
    public object? Tag { get; init; }

    public string? Title { get; init; }
    
    public object? Icon { get; init; }
}

public class SeparatorNavigationItemViewModel : NavigationItemViewModelBase;

public class IconPackNavigationItemViewModel : NavigationItemViewModelBase
{
    public IconPackNavigationItemViewModel(IconPackViewModel iconPack)
    {
        Title = iconPack.Caption;
        Tag = iconPack;
        IconPack = iconPack;
    }

    public IconPackViewModel IconPack { get; }
}

public class AllIconPacksNavigationItemViewModel : NavigationItemViewModelBase
{
    public AllIconPacksNavigationItemViewModel()
    {
        Title = "All Icons";
    }

    public MainViewModel MainViewModel => MainViewModel.Instance;
}

public class SettingsNavigationItem : NavigationItemViewModelBase
{
    public SettingsNavigationItem()
    {
        Title = "Settings";
        Icon = new SymbolIconSource(){Symbol = Symbol.Settings};
        Tag = new SettingsViewModel();
    }
}