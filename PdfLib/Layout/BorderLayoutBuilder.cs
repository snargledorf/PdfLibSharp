using PdfLib.Drawing;
using PdfLib.Elements;

namespace PdfLib.Layout;

internal abstract class BorderLayoutBuilder(IElement element, Size contentSize) : LayoutBuilderBase(element, contentSize), ILayoutBuilder
{
    public override ILayout BuildLayout(Rectangle bounds)
    {
        Pen? borderPen;
        if (Element.BorderColor is { } borderColor)
            borderPen = new Pen(borderColor, Element.BorderWidth ?? 1);
        else
            borderPen = null;
        
        return BuildLayout(bounds, borderPen);
    }

    protected abstract ILayout BuildLayout(Rectangle bounds, Pen? borderPen);
}