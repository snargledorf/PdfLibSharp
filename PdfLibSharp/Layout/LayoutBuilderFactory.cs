using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class LayoutBuilderFactory(IMeasureGraphics measureGraphics) : ILayoutBuilderFactory
{
    public ILayoutBuilder GetLayoutBuilder(IElement element, LayoutScope scope)
    {
        return element switch
        {
            IStackContainer container => container.GetLayoutBuilder(scope, this),
            ITextElement textElement => textElement.GetLayoutBuilder(scope, measureGraphics),
            IImageElement imageElement => imageElement.GetLayoutBuilder(),
            ILineElement lineElement => lineElement.GetLayoutBuilder(),
            _ => throw new ArgumentOutOfRangeException(nameof(element), element, "Unsupported element type")
        };
    }
}