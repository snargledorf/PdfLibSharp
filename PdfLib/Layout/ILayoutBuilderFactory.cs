using PdfLib.Drawing;
using PdfLib.Elements;

namespace PdfLib.Layout;

internal interface ILayoutBuilderFactory
{
    ILayoutBuilder GetLayoutBuilder(IElement element, Font font, StringFormat stringFormat);
}