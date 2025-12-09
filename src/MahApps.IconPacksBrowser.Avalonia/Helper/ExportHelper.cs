using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using Avalonia.Skia;
using IconPacks.Avalonia.BootstrapIcons;
using IconPacks.Avalonia.BoxIcons;
using IconPacks.Avalonia.BoxIcons2;
using IconPacks.Avalonia.CircumIcons;
using IconPacks.Avalonia.Codicons;
using IconPacks.Avalonia.Coolicons;
using IconPacks.Avalonia.Core;
using IconPacks.Avalonia.Entypo;
using IconPacks.Avalonia.EvaIcons;
using IconPacks.Avalonia.FeatherIcons;
using IconPacks.Avalonia.FileIcons;
using IconPacks.Avalonia.Fontaudio;
using IconPacks.Avalonia.FontAwesome;
using IconPacks.Avalonia.FontAwesome5;
using IconPacks.Avalonia.FontAwesome6;
using IconPacks.Avalonia.Fontisto;
using IconPacks.Avalonia.ForkAwesome;
using IconPacks.Avalonia.GameIcons;
using IconPacks.Avalonia.Ionicons;
using IconPacks.Avalonia.JamIcons;
using IconPacks.Avalonia.KeyruneIcons;
using IconPacks.Avalonia.Lucide;
using IconPacks.Avalonia.Material;
using IconPacks.Avalonia.MaterialDesign;
using IconPacks.Avalonia.MaterialLight;
using IconPacks.Avalonia.MemoryIcons;
using IconPacks.Avalonia.Microns;
using IconPacks.Avalonia.MingCuteIcons;
using IconPacks.Avalonia.Modern;
using IconPacks.Avalonia.MynaUIIcons;
using IconPacks.Avalonia.Octicons;
using IconPacks.Avalonia.PhosphorIcons;
using IconPacks.Avalonia.PicolIcons;
using IconPacks.Avalonia.PixelartIcons;
using IconPacks.Avalonia.RadixIcons;
using IconPacks.Avalonia.RemixIcon;
using IconPacks.Avalonia.RPGAwesome;
using IconPacks.Avalonia.SimpleIcons;
using IconPacks.Avalonia.Typicons;
using IconPacks.Avalonia.Unicons;
using IconPacks.Avalonia.VaadinIcons;
using IconPacks.Avalonia.WeatherIcons;
using IconPacks.Avalonia.Zondicons;
using MahApps.IconPacksBrowser.Avalonia.Properties;
using MahApps.IconPacksBrowser.Avalonia.ViewModels;
using SkiaSharp;

namespace MahApps.IconPacksBrowser.Avalonia.Helper;

internal static class ExportHelper
{
    // SVG-File
    private static string? _svgFileTemplate;

    internal static string SvgFileTemplate => _svgFileTemplate ??= LoadTemplateString("SVG.xml");

    // XAML-File (WPF)
    private static string? _wpfFileTemplate;

    internal static string WpfFileTemplate => _wpfFileTemplate ??= LoadTemplateString("WPF.xml");

    // XAML-File (WPF)
    private static string? _uwpFileTemplate;

    internal static string UwpFileTemplate => _uwpFileTemplate ??= LoadTemplateString("WPF.xml");

    // Clipboard - WPF
    private static string? _clipboardWpf;

    internal static string ClipboardWpf => _clipboardWpf ??= LoadTemplateString("Clipboard.WPF.xml");

    // Clipboard - WPF
    private static string? _clipboardWpfGeometry;

    internal static string ClipboardWpfGeometry => _clipboardWpfGeometry ??= LoadTemplateString("Clipboard.WPF.Geometry.xml");

    // Clipboard - UWP
    private static string? _clipboardUwp;

    internal static string ClipboardUwp => _clipboardUwp ??= LoadTemplateString("Clipboard.UWP.xml");

    // Clipboard - Content
    private static string? _clipboardContent;

    internal static string ClipboardContent => _clipboardContent ??= LoadTemplateString("Clipboard.Content.xml");

    // Clipboard - PathData
    private static string? _clipboardData;

    internal static string ClipboardData => _clipboardData ??= LoadTemplateString("Clipboard.PathData.xml");

    internal static string FillTemplate(string template, ExportParameters parameters)
    {
        return template.Replace("@IconKind", parameters.IconKind)
            .Replace("@IconPackName", parameters.IconPackName)
            .Replace("@IconPackHomepage", parameters.IconPackHomepage)
            .Replace("@IconPackLicense", parameters.IconPackLicense)
            .Replace("@PageWidth", parameters.PageWidth)
            .Replace("@PageHeight", parameters.PageHeight)
            .CheckedReplace("@PathData", () => parameters.PathData) // avoid allocation of Lazy<string>
            .Replace("@FillColor", parameters.FillColor)
            .Replace("@Background", parameters.Background)
            .Replace("@StrokeColor", parameters.StrokeColor)
            .Replace("@StrokeWidth", parameters.StrokeWidth)
            .Replace("@StrokeLineCap", parameters.StrokeLineCap)
            .Replace("@StrokeLineJoin", parameters.StrokeLineJoin)
            .Replace("@TransformMatrix", parameters.TransformMatrix);
    }

    internal static string LoadTemplateString(string fileName)
    {
        // In Browser/WASM, prefer embedded assets and avoid any File.* APIs.
        if (OperatingSystem.IsBrowser())
        {
            var uri = new Uri($"avares://MahApps.IconPacksBrowser.Avalonia/Assets/ExportTemplates/{fileName}", UriKind.RelativeOrAbsolute);
            using var stream = AssetLoader.Open(uri);
            using var streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }

        if (string.IsNullOrWhiteSpace(Settings.Default.ExportTemplatesDir) ||
            !File.Exists(Path.Combine(Settings.Default.ExportTemplatesDir, fileName)))
        {
            var uri = new Uri($"avares://MahApps.IconPacksBrowser.Avalonia/Assets/ExportTemplates/{fileName}", UriKind.RelativeOrAbsolute);
            using var stream = AssetLoader.Open(uri);
            using var streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }
        else
        {
            return File.ReadAllText(Path.Combine(Settings.Default.ExportTemplatesDir, fileName));
        }
    }

    // Make trimming/AOT-safe by avoiding reflection entirely. We pattern-match the known icon-pack enum
    // types and call a generic helper that accesses PackIconDataFactory<TEnum> directly, so the linker
    // can see all needed generic instantiations.
    internal static SKPath? GetPath(Enum kind)
    {
        try
        {
            SKPath? skPath = kind switch
            {
                PackIconBootstrapIconsKind k => GetPathCore(k),
                PackIconBoxIconsKind k => GetPathCore(k),
                PackIconBoxIcons2Kind k => GetPathCore(k),
                PackIconCircumIconsKind k => GetPathCore(k),
                PackIconCodiconsKind k => GetPathCore(k),
                PackIconCooliconsKind k => GetPathCore(k),
                PackIconEntypoKind k => GetPathCore(k),
                PackIconEvaIconsKind k => GetPathCore(k),
                PackIconFeatherIconsKind k => GetPathCore(k),
                PackIconFileIconsKind k => GetPathCore(k),
                PackIconFontaudioKind k => GetPathCore(k),
                PackIconFontAwesomeKind k => GetPathCore(k),
                PackIconFontAwesome5Kind k => GetPathCore(k),
                PackIconFontAwesome6Kind k => GetPathCore(k),
                PackIconFontistoKind k => GetPathCore(k),
                PackIconForkAwesomeKind k => GetPathCore(k),
                PackIconGameIconsKind k => GetPathCore(k),
                PackIconIoniconsKind k => GetPathCore(k),
                PackIconJamIconsKind k => GetPathCore(k),
                PackIconKeyruneIconsKind k => GetPathCore(k),
                PackIconLucideKind k => GetPathCore(k),
                PackIconMaterialKind k => GetPathCore(k),
                PackIconMaterialDesignKind k => GetPathCore(k),
                PackIconMaterialLightKind k => GetPathCore(k),
                PackIconMemoryIconsKind k => GetPathCore(k),
                PackIconMicronsKind k => GetPathCore(k),
                PackIconMingCuteIconsKind k => GetPathCore(k),
                PackIconModernKind k => GetPathCore(k),
                PackIconMynaUIIconsKind k => GetPathCore(k),
                PackIconOcticonsKind k => GetPathCore(k),
                PackIconPhosphorIconsKind k => GetPathCore(k),
                PackIconPicolIconsKind k => GetPathCore(k),
                PackIconPixelartIconsKind k => GetPathCore(k),
                PackIconRadixIconsKind k => GetPathCore(k),
                PackIconRemixIconKind k => GetPathCore(k),
                PackIconRPGAwesomeKind k => GetPathCore(k),
                PackIconSimpleIconsKind k => GetPathCore(k),
                PackIconTypiconsKind k => GetPathCore(k),
                PackIconUniconsKind k => GetPathCore(k),
                PackIconVaadinIconsKind k => GetPathCore(k),
                PackIconWeatherIconsKind k => GetPathCore(k),
                PackIconZondiconsKind k => GetPathCore(k),
                _ => null
            };

#if DEBUG
            if (skPath is null)
            {
                throw new MissingMemberException($"No path found for {kind}");
            }
#else
            if (skPath is null)
                return null;
#endif

            // Transform if needed
            // TODO: Would be great to have an upstream API to get this information to not duplicate the code elsewhere
            switch (kind)
            {
                case PackIconBootstrapIconsKind:
                case PackIconBoxIconsKind:
                case PackIconCodiconsKind:
                case PackIconCooliconsKind:
                case PackIconEvaIconsKind:
                case PackIconFileIconsKind:
                case PackIconFontaudioKind:
                case PackIconFontistoKind:
                case PackIconForkAwesomeKind:
                case PackIconJamIconsKind:
                case PackIconLucideKind:
                case PackIconMingCuteIconsKind:
                case PackIconMynaUIIconsKind:
                case PackIconRPGAwesomeKind:
                case PackIconTypiconsKind:
                case PackIconVaadinIconsKind:
                    skPath.Transform(SKMatrix.CreateScale(1, -1));
                    break;
            }

            skPath.FillType = SKPathFillType.EvenOdd;

            return skPath;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static SKPath? GetPathCore<TEnum>(TEnum kind)
        where TEnum : struct, Enum
    {
        // Access the typed data index directly without reflection. The Value is expected to be a
        // dictionary mapping enum -> SVG path data string.
        var dictionary = PackIconDataFactory<TEnum>.DataIndex.Value;
        return dictionary.TryGetValue(kind, out var data)
            ? SKPath.ParseSvgPathData(data)
            : null;
    }

    internal static SKPath MoveIntoBounds(this SKPath path, float width, float height, int padding)
    {
        var scale = Math.Max(width - padding * 2, height - padding * 2)
                    / Math.Max(path.Bounds.Width, path.Bounds.Height);

        path.Transform(SKMatrix.CreateScale(scale, scale));
        path.Transform(SKMatrix.CreateTranslation(
            -path.Bounds.Left - (path.Bounds.Width - width) / 2,
            -path.Bounds.Top - (path.Bounds.Height - height) / 2));

        return path;
    }

    internal static async Task SaveAsSvgAsync(IIconViewModel icon)
    {
        var saveFile = await MainViewModel.Instance.SaveFileDialogAsync(filters: new[]
            {
                FilePickerHelper.ImageSvg
            }, fileNameSuggestion: $"{icon.IconPackName}-{icon.Name}",
            defaultExtension: ".svg");

        if (saveFile is null)
            return;

        await using var saveFileStream = await saveFile.OpenWriteAsync();

        var fileContent = FillTemplate(SvgFileTemplate, new ExportParameters(icon));

        if (saveFileStream is { CanWrite: true } && fileContent is { Length: > 0 })
        {
            await using var streamWriter = new StreamWriter(saveFileStream);
            await streamWriter.WriteAsync(fileContent);
        }
    }

    internal static async Task SaveAsWpfXamlAsync(IIconViewModel icon)
    {
        var saveFile = await MainViewModel.Instance.SaveFileDialogAsync(filters: new[]
            {
                FilePickerHelper.XamlWpf
            }, fileNameSuggestion: $"{icon.IconPackName}-{icon.Name}",
            defaultExtension: ".xaml");

        if (saveFile is null)
            return;

        await using var saveFileStream = await saveFile.OpenWriteAsync();

        var fileContent = FillTemplate(WpfFileTemplate, new ExportParameters(icon));

        if (saveFileStream is { CanWrite: true } && fileContent is { Length: > 0 })
        {
            await using var streamWriter = new StreamWriter(saveFileStream);
            await streamWriter.WriteAsync(fileContent);
        }
    }

    internal static async Task SaveAsAvaloniaXamlAsync(IIconViewModel icon)
    {
        var saveFile = await MainViewModel.Instance.SaveFileDialogAsync(filters: new[]
            {
                FilePickerHelper.XamlAvalonia
            }, fileNameSuggestion: $"{icon.IconPackName}-{icon.Name}",
            defaultExtension: ".axaml");

        if (saveFile is null)
            return;

        await using var saveFileStream = await saveFile.OpenWriteAsync();

        var fileContent = FillTemplate(WpfFileTemplate, new ExportParameters(icon));

        if (saveFileStream is { CanWrite: true } && fileContent is { Length: > 0 })
        {
            await using var streamWriter = new StreamWriter(saveFileStream);
            await streamWriter.WriteAsync(fileContent);
        }
    }

    internal static async Task SaveAsBitmapAsync(IIconViewModel icon)
    {
        var saveFile = await MainViewModel.Instance.SaveFileDialogAsync(filters: new[]
            {
                FilePickerFileTypes.ImagePng,
                FilePickerFileTypes.ImageJpg,
                FilePickerFileTypes.ImageWebp,
                FilePickerHelper.ImageBmp,
            }, fileNameSuggestion: $"{icon.IconPackName}-{icon.Name}",
            defaultExtension: ".png");

        if (saveFile is null)
            return;

        await using var saveFileStream = await saveFile.OpenWriteAsync();

        int renderWidth = Settings.Default.IconPreviewSize;
        int renderHeight = Settings.Default.IconPreviewSize;
        int padding = Settings.Default.IconPreviewPadding;

        using var path = GetPath(icon.Value)?.MoveIntoBounds(renderWidth, renderHeight, padding);

        using var bitmap = new SKBitmap(new SKImageInfo(renderWidth, renderHeight));
        using var canvas = new SKCanvas(bitmap);
        using var paint = new SKPaint();

        paint.IsAntialias = true;

        if (Settings.Default.IconBackground.A > 0)
        {
            canvas.DrawColor(Settings.Default.IconBackground.ToSKColor());
        }

        paint.Color = Settings.Default.IconForeground.ToSKColor();
        paint.IsStroke = icon.Value is PackIconFeatherIconsKind;

        canvas.DrawPath(path, paint);

        var encoding = Path.GetExtension(saveFile.Name) switch
        {
            ".jpg" => SKEncodedImageFormat.Jpeg,
            ".webp" => SKEncodedImageFormat.Webp,
            ".bmp" => SKEncodedImageFormat.Bmp,
            _ => SKEncodedImageFormat.Png
        };

        if (saveFileStream is { CanWrite: true })
        {
            using var image = SKImage.FromBitmap(bitmap);
            using var encodedImage = image.Encode(encoding, 100);
            encodedImage.SaveTo(saveFileStream);
        }
    }
}

internal struct ExportParameters
{
    /// <summary>
    /// Provides a default set of Export parameters. You should edit this to your needs.
    /// </summary>
    /// <param name="icon"></param>
    internal ExportParameters(IIconViewModel icon)
    {
        var metaData = icon.MetaData;

        this.IconKind = icon.Name;
        this.IconPackName = icon.IconPackType.Name.Replace("PackIcon", "");
        this.PageWidth = Settings.Default.IconPreviewSize.ToString(CultureInfo.InvariantCulture);
        this.PageHeight = Settings.Default.IconPreviewSize.ToString(CultureInfo.InvariantCulture);
        this.FillColor = Settings.Default.IconForeground.ToString();
        this.Background = Settings.Default.IconBackground.ToString();
        this.StrokeColor = Settings.Default.IconForeground.ToString();
        this.StrokeWidth = icon.Value is PackIconFeatherIconsKind ? "2" : "0"; // TODO: We need an API to read these values
        this.StrokeLineCap = nameof(PenLineCap.Round);
        this.StrokeLineJoin = nameof(PenLineJoin.Round);
        this.TransformMatrix = Matrix.Identity.ToString(); // TODO Get correct Matrix

        this.IconPackHomepage = metaData.ProjectUrl;
        this.IconPackLicense = metaData.LicenseUrl;

        this.PathData = ExportHelper.GetPath(icon.Value)?
            .MoveIntoBounds(Settings.Default.IconPreviewSize, Settings.Default.IconPreviewSize, Settings.Default.IconPreviewPadding)
            .ToSvgPathData() ?? string.Empty;
    }

    internal string IconKind { get; set; }
    internal string IconPackName { get; set; }
    internal string? IconPackHomepage { get; set; }
    internal string? IconPackLicense { get; set; }
    internal string PageWidth { get; set; }
    internal string PageHeight { get; set; }
    internal string? PathData { get; set; }
    internal string FillColor { get; set; }
    internal string Background { get; set; }
    internal string StrokeColor { get; set; }
    internal string StrokeWidth { get; set; }
    internal string StrokeLineCap { get; set; }
    internal string StrokeLineJoin { get; set; }
    internal string TransformMatrix { get; set; }
}

internal static class ExportHelperExtensions
{
    internal static string CheckedReplace(this string input, string oldValue, Func<string?> newValue)
    {
        if (input.Contains(oldValue))
        {
            return input.Replace(oldValue, newValue());
        }

        return input;
    }
}