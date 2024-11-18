using PdfLib.Drawing;
using PdfLib.Elements;
using PdfLib.Elements.Content;
using PdfLib.Elements.Layout;

namespace PdfLib.Layout;

internal class LayoutGenerator(IMeasureGraphics measureGraphics) : ILayoutGenerator
{
    private readonly LayoutBuilderFactory _layoutBuilderFactory = new(measureGraphics);

    public IMeasureGraphics MeasureGraphics { get; } = measureGraphics;

    public ILayout GenerateLayout(IElement element, Rectangle bounds, LayoutScope scope)
    {
        ILayoutBuilder layoutBuilder = _layoutBuilderFactory.GetLayoutBuilder(element, scope);
        return layoutBuilder.BuildLayout(bounds);
    }
}