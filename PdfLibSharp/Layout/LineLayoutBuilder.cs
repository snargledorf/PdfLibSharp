using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class LineLayoutFactory(ILineElement lineElement) 
    : ElementLayoutFactory(lineElement)
{
    protected override ILayout CreateInnerLayout(Size constraints)
    {
        Size size;
        
        if (lineElement.Direction == Direction.Horizontal)
        {
            size = constraints with
            {
                Height = lineElement.Pen.Width
            };
        }
        else
        {
            size = constraints with
            {
                Width = lineElement.Pen.Width
            };
        }

        return new LineLayout(lineElement, size, lineElement.Pen);
    }
}