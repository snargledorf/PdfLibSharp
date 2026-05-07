namespace PdfLibSharp.Elements;

[Flags]
public enum FontStyles
{
    Normal,
    Bold,
    Italic,
    BoldItalic = Bold | Italic,
    Underline,
    Strikeout,
}

internal static class FontStylesExtensions
{
    public static bool HasFlagFast(this FontStyles value, FontStyles flag)
    {
        return (value & flag) != 0;
    }
}