using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record TextContent(IReadOnlyCollection<PositionedTextLine> Lines, Font Font, StringFormat Format, Brush Brush, Pen? BorderPen) 
    : BorderedContent(BorderPen);