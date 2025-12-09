using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia.Media;
using MahApps.IconPacksBrowser.Avalonia.Properties;

namespace MahApps.IconPacksBrowser.Avalonia.Helper;

public static class JsonHelper
{
    public static JsonSerializerOptions SettingsOptions { get; } = new()
    {
        Converters = { new JsonColorConverter() }
    };
}

public class JsonColorConverter : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Color.TryParse(reader.GetString(), out var color) ? color : Colors.Green;
    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}


// System.Text.Json source-generation context for trim/AOT-safe serialization of Settings
[JsonSourceGenerationOptions(WriteIndented = true, GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(Settings))]
[JsonSerializable(typeof(Color))]
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(bool))]
internal partial class SettingsJsonContext : JsonSerializerContext
{
}

