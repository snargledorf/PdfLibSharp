using PdfLib.Drawing;
using PdfLib.Elements;
using PdfLib.Elements.Layout;

namespace PdfLib.Layout;

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