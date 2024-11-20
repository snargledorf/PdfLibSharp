using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record LinePositionedLayout(Pen Pen, Point Start, Point End, Rectangle ContentBounds) : PositionedLayout(ContentBounds);