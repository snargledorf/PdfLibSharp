using PdfLib.Drawing;
using PdfLib.Elements;
using PdfLib.Elements.Content;
using PdfLib.Elements.Layout;

namespace PdfLib.Layout;

internal class LayoutGenerator(IMeasureGraphics measureGraphics) : ILayoutGenerator
{
    private readonly LayoutBuilderFactory _layoutBuilderFactory = new(measureGraphics);

    public IMeasureGraphics MeasureGraphics { get; } = measureGraphics;

    public ILayout GenerateLayout(IElement element, Rectangle bounds, Font font, Color fontColor, StringFormat stringFormat)
    {
        ILayoutBuilder layoutBuilder = _layoutBuilderFactory.GetLayoutBuilder(element, font, fontColor, stringFormat);
        return layoutBuilder.BuildLayout(bounds);
    }
}