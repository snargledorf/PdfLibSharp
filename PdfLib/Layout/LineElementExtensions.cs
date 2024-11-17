using PdfLib.Drawing;
using PdfLib.Elements;

namespace PdfLib.Layout;

internal static class LineElementExtensions
{
    public static ILayoutBuilder GetLayoutBuilder(this ILineElement lineElement)
    {
        var size = new Size
        (
            Width: lineElement.Width ?? lineElement.Pen.Width,
            Height: lineElement.Height ?? lineElement.Pen.Width
        );
        
        return new LineLayoutBuilder(lineElement, size);
    }
}