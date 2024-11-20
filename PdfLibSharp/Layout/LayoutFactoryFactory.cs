using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class LayoutFactoryFactory(IMeasureGraphics measureGraphics) : ILayoutBuilderFactory
{
    public ILayoutFactory CreateLayoutFactory(IElement element, LayoutScope scope)
    {
        return element switch
        {
            IStackContainer container => container.CreateLayoutFactory(scope, this),
            ITextElement textElement => textElement.CreateLayoutFactory(scope, measureGraphics),
            IImageElement imageElement => imageElement.CreateLayoutFactory(),
            ILineElement lineElement => lineElement.CreateLayoutFactory(),
            _ => throw new ArgumentOutOfRangeException(nameof(element), element, "Unsupported element type")
        };
    }

    public void Dispose()
    {
        measureGraphics.Dispose();
    }
}