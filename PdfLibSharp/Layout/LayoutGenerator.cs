using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

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