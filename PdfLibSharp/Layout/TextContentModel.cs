using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record TextContentModel(
    IReadOnlyCollection<TextLine> Lines,
    Size Size,
    Font Font,
    StringFormat Format,
    Brush Brush,
    Pen? BorderPen)
    : BorderElementContentModel(Size, BorderPen);