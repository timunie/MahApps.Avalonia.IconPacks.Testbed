using System;
using System.Globalization;
using System.Reflection;
using Avalonia.Data.Converters;
using MahApps.IconPacksBrowser.Avalonia.ViewModels;

namespace MahApps.IconPacksBrowser.Avalonia.Converters;

public class GetAssemblyVersionConverter : IValueConverter
{
    public static GetAssemblyVersionConverter Instance { get; } = new GetAssemblyVersionConverter();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is IconPackViewModel iconPack)
        {
            return iconPack.IconPacksVersion;
        }
        
        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}