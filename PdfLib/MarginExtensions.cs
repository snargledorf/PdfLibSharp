using PdfLibrary.Drawing;
using PdfLibrary.Elements;

namespace PdfLibrary;

internal static class MarginExtensions
{
    public static Point GetInnerPoint(this IElement element, Point point)
    {
        return point + new Point(element.Margins.Left, element.Margins.Top);
    }

    public static Size GetInnerSize(this IElement element, Size size)
    {
        var marginsSize = new Size
        (
            Width: element.Margins.Left + element.Margins.Right,
            Height: element.Margins.Top + element.Margins.Bottom
        );
        
        return size - marginsSize;
    }
}