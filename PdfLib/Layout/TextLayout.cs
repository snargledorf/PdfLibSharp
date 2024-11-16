using PdfLib.Drawing;
using PdfLib.Elements;

namespace PdfLib.Layout;

internal class TextLayout(
    string text,
    Point point,
    Size contentSize,
    Margins margins,
    Font font,
    StringFormat format,
    Brush brush,
    Pen? borderPen)
    : ElementLayout(point, contentSize, margins, borderPen), ITextLayout
{
    public string Text { get; } = text;
    public StringFormat Format { get; } = format;
    public Brush Brush { get; } = brush;
    public Font Font { get; } = font;
}