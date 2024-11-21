using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record TextContent(Rectangle Bounds, IReadOnlyCollection<PositionedTextLine> Lines, Font Font, StringFormat Format, Brush Brush, Pen? BorderPen) 
    : BorderedContent(Bounds, BorderPen);