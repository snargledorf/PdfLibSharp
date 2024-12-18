using PdfSharp.Drawing;

namespace PdfLibSharp.Elements;

[Flags]
public enum FontStyles
{
    Normal = XFontStyleEx.Regular,
    Bold = XFontStyleEx.Bold,
    Italic = XFontStyleEx.Italic,
    Underline = XFontStyleEx.Underline,
    Strikeout = XFontStyleEx.Strikeout,
    BoldItalic = XFontStyleEx.BoldItalic,
}

internal static class FontStylesExtensions
{
    public static bool HasFlagFast(this FontStyles value, FontStyles flag)
    {
        return (value & flag) != 0;
    }
}