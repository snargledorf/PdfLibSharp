using PdfLibrary.Drawing;
using PdfLibrary.Elements;

namespace PdfLibrary.Layout;

internal interface ILayoutGenerator
{
    IMeasureGraphics MeasureGraphics { get; }
    ILayout GenerateLayout(IElement element, Rectangle bounds, Font font, StringFormat stringFormat);
}