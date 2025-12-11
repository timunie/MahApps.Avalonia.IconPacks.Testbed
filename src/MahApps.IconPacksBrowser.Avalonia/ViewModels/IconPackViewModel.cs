using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        this.IconPacksVersion = GetAssemblyVersionSafe(packType.Assembly);
    }
    
    public async Task<IList<IIconViewModel>> LoadIconsAsync(Type enumType, Type packType)
    {
        var collection = await Task.Run(() => GetIcons(enumType, packType).OrderBy(i => i.Name, StringComparer.InvariantCultureIgnoreCase).ToList());

        this.Icons = new ObservableCollection<IIconViewModel>(collection);
        this.IconCount = collection.Count;

        return Icons;
    }

    [ObservableProperty] 
    public partial IList<IIconViewModel> Icons { get; set; } = [];

    
    [ObservableProperty]
    public partial int IconCount { get; set; }

    private static MetaDataAttribute? GetMetaData(Type packType)
    {
        var metaData = Attribute.GetCustomAttribute(packType, typeof(MetaDataAttribute)) as MetaDataAttribute;
        return metaData;
    }

    private static IEnumerable<IIconViewModel> GetIcons(Type enumType, Type packType)
    {
        var metaData = GetMetaData(packType);
        // AOT-friendly enumeration of enum values:
        // Use GetValuesAsUnderlyingType (safe for AOT) and convert each value back to the enum via Enum.ToObject.
        // Avoid Enum.GetValues(Type) which may require dynamic code (IL3050 in trimmed/AOT builds).
        return Enum.GetValuesAsUnderlyingType(enumType)
            .Cast<object>()
            .Select(v => (Enum)Enum.ToObject(enumType, v))
            .Where(k => k.ToString() != "None")
            .Select(k => new IconViewModel(enumType, packType, k, metaData!));
    }

    public string? Caption { get; }

    public MetaDataAttribute? MetaData { get; }


    /// <summary>
    /// Gets the Version info for this pack
    /// </summary>
    public string? IconPacksVersion { get; }

    private static string? GetAssemblyVersionSafe(Assembly asm)
    {
        // Prefer informational version if present
        var info = asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (!string.IsNullOrWhiteSpace(info))
            return info;

        return asm.GetName().Version?.ToString();
    }
}