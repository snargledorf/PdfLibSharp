using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Content;

namespace PdfLibSharp.Layout;

internal class TextLayout(
    ITextElement textElement,
    IReadOnlyCollection<TextLine> lines,
    Size contentSize,
    Font font,
    StringFormat format,
    Brush brush,
    Pen? borderPen)
    : BorderElementLayout(textElement, contentSize, borderPen), ITextLayout
{
    public IReadOnlyCollection<TextLine> Lines { get; } = lines;
    public StringFormat Format { get; } = format;
    public Brush Brush { get; } = brush;
    public Font Font { get; } = font;

    protected override object BuildContent(Rectangle contentBounds)
    {
        var positionedLines = new List<PositionedTextLine>();
        
        Point linePoint = contentBounds.Point;

        PositionedTextLine? previousLineLayout = null;
        foreach (TextLine line in Lines)
        {
            if (previousLineLayout is not null)
            {
                linePoint = linePoint with
                {
                    Y = previousLineLayout.ContentBounds.Bottom
                };
            }

            Size lineContentSize = line.ContentSize with
            {
                Width = contentBounds.Size.Width
            };
            
            var lineBounds = new Rectangle(linePoint, lineContentSize);
            
            previousLineLayout = new PositionedTextLine(line.Text, lineBounds);
            positionedLines.Add(previousLineLayout);
        }
        
        return new TextContent(positionedLines.ToArray(), Font, Format, Brush, BorderPen);
    }
}