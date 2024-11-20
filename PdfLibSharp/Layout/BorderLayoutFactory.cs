using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal abstract class BorderLayoutFactory(IBorderElement element) : ElementLayoutFactory(element)
{
    public Pen? BorderPen
    {
        get
        {
            if (element.BorderColor is { } borderColor)
                return new Pen(borderColor, element.BorderWidth ?? 1);
            return null;
        }
    }
}