using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Content;

namespace PdfLibSharp.Layout;

internal class ImageLayout(IImageElement imageElement, Size contentSize, Pen? borderPen)
    : BorderElementLayout(imageElement, contentSize, borderPen), IImageLayout
{
    public Image Image { get; } = imageElement.Image;
    
    public override PositionedLayout ToPositionedLayout(Rectangle contentBounds)
    {
        return new ImagePositionedLayout(Image, contentBounds, BorderPen);
    }
}