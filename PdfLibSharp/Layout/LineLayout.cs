using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal class LineLayout(ILineElement lineElement, Size contentSize, Pen linePen) 
    : ElementLayout(lineElement, contentSize), ILineLayout
{
    public Pen Pen { get; } = linePen;

    protected override object BuildContent(Rectangle contentBounds)
    {
        return new LineContent(Pen);
    }
}