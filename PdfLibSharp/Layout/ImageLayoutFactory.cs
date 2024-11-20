using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Content;

namespace PdfLibSharp.Layout;

internal class ImageLayoutFactory(IImageElement imageElement) : BorderLayoutFactory(imageElement)
{
    protected override ILayout CreateInnerLayout(Size constraints)
    {
        // TODO: Resize image to fit constraints
        return new ImageLayout(imageElement, constraints, BorderPen);
    }
}