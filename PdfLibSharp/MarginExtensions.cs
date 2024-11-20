using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp;

internal static class MarginExtensions
{
    public static Point GetInnerPoint(this IElement element, Point point)
    {
        return point + new Point(element.Margins.Left, element.Margins.Top);
    }

    public static Size ToSize(this Margins margins)
    {
        return new Size(margins.Left + margins.Right, margins.Top + margins.Bottom);
    }
}