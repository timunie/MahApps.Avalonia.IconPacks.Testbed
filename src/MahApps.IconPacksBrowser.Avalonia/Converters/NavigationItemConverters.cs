using Avalonia.Data.Converters;
using MahApps.IconPacksBrowser.Avalonia.ViewModels;

namespace MahApps.IconPacksBrowser.Avalonia.Converters;

internal static class NavigationItemConverters
{
    public static FuncValueConverter<NavigationItemViewModelBase, bool> IsEnabledConverter { get; } = 
        new(x => x is not SeparatorNavigationItemViewModel);
    
    public static FuncValueConverter<NavigationItemViewModelBase, int> MinHeightConverter { get; } = 
        new (x => x is SeparatorNavigationItemViewModel ? 0 : 32);
}