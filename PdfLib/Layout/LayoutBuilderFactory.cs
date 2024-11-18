using PdfLib.Drawing;
using PdfLib.Elements;
using PdfLib.Elements.Content;
using PdfLib.Elements.Layout;

namespace PdfLib.Layout;

internal class LayoutBuilderFactory(IMeasureGraphics measureGraphics) : ILayoutBuilderFactory
{
    public ILayoutBuilder GetLayoutBuilder(IElement element, Font font, Color fontColor, StringFormat stringFormat)
    {
        return element switch
        {
            IStackContainer container => container.GetLayoutBuilder(font, fontColor, stringFormat, this),
            ITextElement textElement => textElement.GetLayoutBuilder(font, fontColor, stringFormat, measureGraphics),
            IImageElement imageElement => imageElement.GetLayoutBuilder(),
            ILineElement lineElement => lineElement.GetLayoutBuilder(),
            _ => throw new ArgumentOutOfRangeException(nameof(element), element, "Unsupported element type")
        };
    }
}