using Avalonia.Platform.Storage;

namespace MahApps.IconPacksBrowser.Avalonia.Helper;

public static class FilePickerHelper
{
    public static FilePickerFileType ImageBmp { get; } = new("Bitmap image")
    {
        Patterns = new[] { "*.bmp" },
        AppleUniformTypeIdentifiers = new[] { "com.microsoft.bmp" },
        MimeTypes = new[] { "image/bmp" }
    };
    
    public static FilePickerFileType ImageSvg { get; } = new("SVG image")
    {
        Patterns = new[] { "*.svg" },
        AppleUniformTypeIdentifiers = new[] { "public.svg-image" },
        MimeTypes = new[] { "image/svg+xml" }
    };
    
    public static FilePickerFileType XamlWpf { get; } = new("WPF xaml file")
    {
        Patterns = new[] { "*.xaml" },
        AppleUniformTypeIdentifiers = new[] { "public.xml" },
        MimeTypes = new[] { "application/xml" }
    };
    
    public static FilePickerFileType XamlAvalonia { get; } = new("Avalonia xaml file")
    {
        Patterns = new[] { "*.axaml" },
        AppleUniformTypeIdentifiers = new[] { "public.xml" },
        MimeTypes = new[] { "application/xml" }
    };
}