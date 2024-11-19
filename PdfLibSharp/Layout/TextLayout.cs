using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal class TextLayout(
    IReadOnlyCollection<TextLineLayout> lines,
    Point point,
    Size contentSize,
    Margins margins,
    Font font,
    StringFormat format,
    Brush brush,
    Pen? borderPen)
    : BorderElementLayout(point, contentSize, margins, borderPen), ITextLayout
{
    public IReadOnlyCollection<TextLineLayout> Lines { get; } = lines;
    public StringFormat Format { get; } = format;
    public Brush Brush { get; } = brush;
    public Font Font { get; } = font;
}