using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using IconPacks.Avalonia.Core.Attributes;

namespace MahApps.IconPacksBrowser.Avalonia.ViewModels;

public partial class IconPackViewModel : ViewModelBase
{
    public Type EnumType { get; }
    public Type PackType { get; }
    

    public IconPackViewModel(Type enumType, Type packType)
    {
        EnumType = enumType;
        PackType = packType;
        // Get the Name of the IconPack via Attributes
        this.MetaData = Attribute.GetCustomAttribute(packType, typeof(MetaDataAttribute)) as MetaDataAttribute;

        this.Caption = this.MetaData?.Name;
        this.IconPacksVersion = FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(packType)!.Location).FileVersion;
    }
    
    public async Task<IEnumerable<IIconViewModel>> LoadIconsAsync(Type enumType, Type packType)
    {
        var collection = await Task.Run(() => GetIcons(enumType, packType).OrderBy(i => i.Name, StringComparer.InvariantCultureIgnoreCase).ToList());

        this.Icons = new ObservableCollection<IIconViewModel>(collection);
        this.IconCount = collection.Count;

        return Icons;
    }

    [ObservableProperty]
    private IList<IIconViewModel> _icons = [];

    [ObservableProperty]
    private int _iconCount;
    
    
    private static MetaDataAttribute? GetMetaData(Type packType)
    {
        var metaData = Attribute.GetCustomAttribute(packType, typeof(MetaDataAttribute)) as MetaDataAttribute;
        return metaData;
    }

    private static IEnumerable<IIconViewModel> GetIcons(Type enumType, Type packType)
    {
        var metaData = GetMetaData(packType);
        return Enum.GetValues(enumType)
            .OfType<Enum>()
            .Where(k => k.ToString() != "None")
            .Select(k => new IconViewModel(enumType, packType, k, metaData!));
    }

    public string? Caption { get; }

    public MetaDataAttribute? MetaData { get; }


    /// <summary>
    /// Gets the Version info for this pack
    /// </summary>
    public string? IconPacksVersion { get; }
}