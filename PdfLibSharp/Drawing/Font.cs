using PdfLibSharp.Elements;
using PdfSharp.Drawing;

namespace PdfLibSharp.Drawing;

public record Font(string Family, double Size, FontStyles FontStyles = FontStyles.Normal)
{
    public static implicit operator XFont(Font font) => new(font.Family, font.Size, (XFontStyleEx)font.FontStyles);
}