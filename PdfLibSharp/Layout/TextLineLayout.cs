using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal class TextLineLayout(string text, Size contentSize) : ILayout
{
    public string Text { get; } = text;

    public Margins Margins { get; } = new();
    
    public Size ContentSize { get; } = contentSize;

    public PositionedLayout ToPositionedLayout(Rectangle contentBounds) =>
        new TextLinePositionedLayout(Text, contentBounds);
}