using IconPacks.Avalonia.Core.Attributes;
using IconPacks.Avalonia.MaterialDesign;

namespace MahApps.IconPacksBrowser.Avalonia.ViewModels;

public static class _DesignData
{
    public static IIconViewModel IconPreviewData { get; } = new IconViewModel(typeof(PackIconMaterialDesignKind),
        typeof(PackIconMaterialDesignKind),
        PackIconMaterialDesignKind.AccountBox, new MetaDataAttribute("Hello", "world", "??"));
}