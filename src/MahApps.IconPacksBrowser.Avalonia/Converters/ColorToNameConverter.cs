using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using IconPacks.Avalonia.Core.Converter;

namespace MahApps.IconPacksBrowser.Avalonia.Converters;

public class ColorToNameConverter : MarkupConverter, IValueConverter
{
    static ColorToNameConverter()
    {
        AccentColorNames = new Dictionary<Color, string>();
        AccentColorNames.TryAdd(Color.Parse("#FF0050EF"), "Cobalt");
        AccentColorNames.TryAdd(Color.Parse("#FF0078D7"), "Blue");
        AccentColorNames.TryAdd(Color.Parse("#FF008A00"), "Emerald");
        AccentColorNames.TryAdd(Color.Parse("#FF00ABA9"), "Teal");
        AccentColorNames.TryAdd(Color.Parse("#FF1BA1E2"), "Cyan");
        AccentColorNames.TryAdd(Color.Parse("#FF60A917"), "Green");
        AccentColorNames.TryAdd(Color.Parse("#FF6459DF"), "Purple");
        AccentColorNames.TryAdd(Color.Parse("#FF647687"), "Steel");
        AccentColorNames.TryAdd(Color.Parse("#FF6A00FF"), "Indigo");
        AccentColorNames.TryAdd(Color.Parse("#FF6D8764"), "Olive");
        AccentColorNames.TryAdd(Color.Parse("#FF76608A"), "Mauve");
        AccentColorNames.TryAdd(Color.Parse("#FF825A2C"), "Brown");
        AccentColorNames.TryAdd(Color.Parse("#FF87794E"), "Taupe");
        AccentColorNames.TryAdd(Color.Parse("#FFA0522D"), "Sienna");
        AccentColorNames.TryAdd(Color.Parse("#FFA20025"), "Crimson");
        AccentColorNames.TryAdd(Color.Parse("#FFA4C400"), "Lime");
        AccentColorNames.TryAdd(Color.Parse("#FFAA00FF"), "Violet");
        AccentColorNames.TryAdd(Color.Parse("#FFD80073"), "Magenta");
        AccentColorNames.TryAdd(Color.Parse("#FFE51400"), "Red");
        AccentColorNames.TryAdd(Color.Parse("#FFF0A30A"), "Amber");
        AccentColorNames.TryAdd(Color.Parse("#FFF472D0"), "Pink");
        AccentColorNames.TryAdd(Color.Parse("#FFFA6800"), "Orange");
        AccentColorNames.TryAdd(Color.Parse("#FFFEDE06"), "Yellow");
    }

    public static Dictionary<Color, string> AccentColorNames { get; }

    protected override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return null;
        var color = (Color)value;
        
        return AccentColorNames.TryGetValue(color,  out var name) ? name : value;
    }

    protected override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}