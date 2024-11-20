using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record TextPositionedLayout(IReadOnlyCollection<TextLinePositionedLayout> Lines, Font Font, StringFormat Format, Brush Brush, Pen? BorderPen, Rectangle ContentBounds) : PositionedLayout(ContentBounds);