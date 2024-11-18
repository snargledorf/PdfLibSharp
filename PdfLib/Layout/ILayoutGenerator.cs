using PdfLib.Drawing;
using PdfLib.Elements;

namespace PdfLib.Layout;

internal interface ILayoutGenerator
{
    IMeasureGraphics MeasureGraphics { get; }
    ILayout GenerateLayout(IElement element, Rectangle bounds, Font font, Color fontColor, StringFormat stringFormat);
}