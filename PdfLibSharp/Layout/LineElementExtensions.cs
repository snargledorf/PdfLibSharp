using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal static class LineElementExtensions
{
    public static ILayoutFactory CreateLayoutFactory(this ILineElement lineElement)
    {
        return new LineLayoutFactory(lineElement);
    }
}