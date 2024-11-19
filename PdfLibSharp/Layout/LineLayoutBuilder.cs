using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class LineLayoutBuilder(ILineElement lineElement, Size contentSize) 
    : LayoutBuilderBase(lineElement, contentSize)
{
    public override ILayout BuildLayout(Rectangle bounds)
    {
        Size size = ContentSize;
        
        if (lineElement.Direction == Direction.Horizontal)
        {
            size = size with
            {
                Width = bounds.Size.Width
            };
        }
        else
        {
            size = size with
            {
                Height = bounds.Size.Height
            };
        }

        Point end = bounds.Point + bounds.Size;

        return new LineLayout(bounds.Point, end, lineElement.Pen, size, lineElement.Margins);
    }
}