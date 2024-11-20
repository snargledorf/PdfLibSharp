using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal sealed record TextLinePositionedLayout(string Text, Rectangle ContentBounds) : PositionedLayout(ContentBounds);