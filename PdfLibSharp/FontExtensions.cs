using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp;

internal static class FontExtensions
{
    public static Font GetFont(this IFont fontElement, Font baseFont)
    {
        Font font = baseFont;
        
        if (fontElement.FontFamily is { } pageFontFamily)
        {
            font = font with
            {
                Family = pageFontFamily,
            };
        }

        if (fontElement.FontSize.HasValue)
        {
            font = font with
            {
                Size = fontElement.FontSize.Value,
            };
        }

        if (fontElement.FontStyles.HasValue)
        {
            font = font with
            {
                FontStyles = fontElement.FontStyles.Value,
            };
        }

        return font;
    }
}