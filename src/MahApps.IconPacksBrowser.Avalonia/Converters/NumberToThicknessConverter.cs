using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using IconPacks.Avalonia.Core.Converter;

namespace MahApps.IconPacksBrowser.Avalonia.Converters;

public class NumberToThicknessConverter : MarkupConverter
{
    protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int val)
        {
            return new Thickness(val);
        }
        return BindingOperations.DoNothing;
    }

    protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Thickness thickness)
        {
            return thickness.Left;
        }

        return BindingOperations.DoNothing;
    }
}