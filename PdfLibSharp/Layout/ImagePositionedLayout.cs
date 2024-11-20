using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record ImagePositionedLayout(Image Image, Rectangle ContentBounds, Pen? BorderPen) : BorderPositionedLayout(ContentBounds, BorderPen);