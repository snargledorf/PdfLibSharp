using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal class TextLineLayout(string text, Rectangle bounds) : ILayout
{
    public string Text { get; } = text;
    public Rectangle OuterBounds { get; } = bounds;
    public Rectangle ContentBounds { get; } = bounds;
}