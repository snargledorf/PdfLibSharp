using PdfLib.Drawing;
using PdfLib.Elements;
using PdfLib.Elements.Content;
using PdfLib.Elements.Layout;

namespace PdfLib.Layout;

internal class LayoutGenerator(IMeasureGraphics measureGraphics) : ILayoutGenerator
{
    public IMeasureGraphics MeasureGraphics { get; } = measureGraphics;

    public ILayout GenerateLayout(IElement element, Rectangle bounds, Font font, StringFormat stringFormat)
    {
        ILayoutBuilder layoutBuilder = GetLayoutBuilder(element, font, stringFormat);
        return layoutBuilder.BuildLayout(bounds);
    }

    private ILayoutBuilder GetLayoutBuilder(IElement element, Font font, StringFormat stringFormat)
    {
        return element switch
        {
            IStackContainer container => container.GetLayoutBuilder(font, stringFormat, MeasureGraphics),
            ITextElement textElement => textElement.GetLayoutBuilder(font, stringFormat, MeasureGraphics),
            IImageElement imageElement => imageElement.GetLayoutBuilder(),
            _ => throw new ArgumentOutOfRangeException(nameof(element), element, "Unsupported element type")
        };
    }
}