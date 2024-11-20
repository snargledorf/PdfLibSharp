using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record BorderPositionedLayout(Rectangle ContentBounds, Pen? BorderPen) : PositionedLayout(ContentBounds);