using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal class LineLayout(ILineElement lineElement, Size contentSize, Pen linePen) 
    : ElementLayout(lineElement, contentSize), ILineLayout
{
    public Pen Pen { get; } = linePen;
    
    public override PositionedLayout ToPositionedLayout(Rectangle contentBounds)
    {
        Point start = contentBounds.Point;

        Point end = contentBounds.BottomRight;
        
        return new LinePositionedLayout(Pen, start, end, contentBounds);
    }
}