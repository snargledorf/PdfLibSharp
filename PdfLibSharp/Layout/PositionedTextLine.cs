using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal sealed record PositionedTextLine(string Text, Rectangle ContentBounds);