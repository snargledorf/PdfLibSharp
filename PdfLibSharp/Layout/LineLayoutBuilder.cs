using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class LineLayoutModelFactory
    : BaseLayoutModelFactory
{
    protected override ContentModel CreateContentModel(IElement element, Size constraints, LayoutScope scope)
    {
        if (element is not LineElement lineElement)
            throw new ArgumentException("Element must implement LineElement", nameof(element));
        
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

        return new LineContentModel(size, lineElement.Direction, lineElement.Pen);
    }
}