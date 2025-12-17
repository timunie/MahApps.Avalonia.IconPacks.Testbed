using Avalonia.Media;

namespace MahApps.IconPacksBrowser.Avalonia.Helper;

public static class ColorHelper
{
    public static Color GetIdealForeground(Color accent)
    {
        if (accent == default)
            return Colors.Black;

        // Compute perceived luminance (sRGB). Using the YIQ approximation for speed.
        // Range: 0..255; threshold ~128 for black/white switch.
        
        var yiq = ((accent.R * 299) + (accent.G * 587) + (accent.B * 114)) / 1000;
        return yiq >= 128 ? Colors.Black : Colors.White;
    }
    
    /// <summary>
    /// Creates a lighter or darker variant of the given accent color based on the current theme.
    /// </summary>
    /// <param name="accent">The base accent color.</param>
    /// <param name="percentage">
    /// The adjustment amount. Accepts 0..1 (e.g. 0.2 == 20%) or 0..100 (e.g. 20 == 20%).
    /// </param>
    /// <param name="isDarkMode">
    /// When true (dark mode), the color will be brightened; when false (light mode), the color will be darkened.
    /// </param>
    /// <returns>The adjusted color.</returns>
    public static Color AdjustAccent(this Color accent, double percentage, bool isDarkMode)
    {
        if (accent == default)
            return accent;

        // Normalize percentage: accept both 0..1 and 0..100 inputs.
        var amount = percentage > 1 ? percentage / 100.0 : percentage;
        amount = Clamp01(amount);

        var hsl = accent.ToHsl();

        double newL;
        if (isDarkMode)
        {
            // Brighten: move L towards 1 by given amount.
            newL = Brighten(hsl.L, amount);
        }
        else
        {
            // Darken: scale L down by (1 - amount).
            newL = hsl.L * (1.0 - amount);
        }

        return HslColor.FromHsl(hsl.H, hsl.S, newL).ToRgb();

        static double Clamp01(double v) => v < 0 ? 0 : (v > 1 ? 1 : v);
        static double Brighten(double l, double amt) => Clamp01(l + (1.0 - l) * amt);
    }
}