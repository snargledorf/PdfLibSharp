using PdfLib.Drawing;
using PdfLib.Elements;

namespace PdfLib;

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