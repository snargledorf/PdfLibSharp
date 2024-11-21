using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal abstract class BorderLayoutModelFactory : BaseLayoutModelFactory
{
    protected override ContentModel CreateContentModel(IElement element, Size constraints, LayoutScope scope)
    {
        if (element is not IBorderElement borderElement)
            throw new ArgumentException("Element must implement IBorderElement", nameof(element));

        Pen? borderPen = GetBorderPen(borderElement);
        
        return CreateBorderedContentModel(borderElement, borderPen, constraints, scope);
    }

    protected abstract ContentModel CreateBorderedContentModel(IBorderElement borderElement, Pen? borderPen, Size constraints, LayoutScope scope);

    private static Pen? GetBorderPen(IBorderElement borderElement)
    {
        if (borderElement.BorderColor is { } borderColor)
            return new Pen(borderColor, borderElement.BorderWidth ?? 1);
        return null;
    }
}