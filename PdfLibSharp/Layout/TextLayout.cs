using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Content;

namespace PdfLibSharp.Layout;

internal class TextLayout(
    ITextElement textElement,
    IReadOnlyCollection<TextLineLayout> lines,
    Size contentSize,
    Font font,
    StringFormat format,
    Brush brush,
    Pen? borderPen)
    : BorderElementLayout(textElement, contentSize, borderPen), ITextLayout
{
    public IReadOnlyCollection<TextLineLayout> Lines { get; } = lines;
    public StringFormat Format { get; } = format;
    public Brush Brush { get; } = brush;
    public Font Font { get; } = font;

    public override PositionedLayout ToPositionedLayout(Rectangle contentBounds)
    {
        var positionedLines = new List<TextLinePositionedLayout>();
        
        Point linePoint = contentBounds.Point;
        foreach (TextLineLayout line in Lines)
        {
            var lineBounds = new Rectangle(linePoint, line.ContentSize);
            var positionedLayout = new TextLinePositionedLayout(line.Text, lineBounds);
            positionedLines.Add(positionedLayout);
            
            linePoint = linePoint with
            {
                Y = linePoint.Y + line.ContentSize.Height
            };
        }
        
        return new TextPositionedLayout(positionedLines.ToArray(), Font, Format, Brush, BorderPen, contentBounds);
    }
}