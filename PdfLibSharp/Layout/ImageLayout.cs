using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Content;

namespace PdfLibSharp.Layout;

internal class ImageLayout(IImageElement imageElement, Size contentSize, Pen? borderPen)
    : BorderElementLayout(imageElement, contentSize, borderPen), IImageLayout
{
    public Image Image { get; } = imageElement.Image;

    protected override object BuildContent(Rectangle contentBounds)
    {
        return new ImageContent(Image, BorderPen);
    }
}