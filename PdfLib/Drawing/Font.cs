using PdfLibrary.Elements;
using PdfSharp.Drawing;

namespace PdfLibrary.Drawing;

public record Font(string Family, double Size, FontStyles FontStyles = FontStyles.Normal)
{
    public static implicit operator XFont(Font font) => new(font.Family, font.Size, (XFontStyleEx)font.FontStyles);
}