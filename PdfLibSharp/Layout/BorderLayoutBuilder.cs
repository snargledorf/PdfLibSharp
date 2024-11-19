using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal abstract class BorderLayoutBuilder(IBorderElement element, Size contentSize) : LayoutBuilderBase(element, contentSize), ILayoutBuilder
{
    public override ILayout BuildLayout(Rectangle bounds)
    {
        Pen? borderPen;
        if (element.BorderColor is { } borderColor)
            borderPen = new Pen(borderColor, element.BorderWidth ?? 1);
        else
            borderPen = null;
        
        return BuildLayout(bounds, borderPen);
    }

    protected abstract ILayout BuildLayout(Rectangle bounds, Pen? borderPen);
}