using PdfLib.Drawing;

namespace PdfLib.Layout;

internal class LayoutScope(Font font, StringFormat stringFormat, Color fontColor)
{
    public Font Font { get; } = font;
    public StringFormat StringFormat { get; } = stringFormat;
    public Color FontColor { get; } = fontColor;
}