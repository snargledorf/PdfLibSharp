using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal sealed record PositionedLayout(Rectangle ContentBounds, Rectangle OuterBounds, object Content);