using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record ContainerPositionedLayout(IReadOnlyList<PositionedLayout> Children, Rectangle ContentBounds, Pen? BorderPen) : BorderPositionedLayout(ContentBounds, BorderPen);