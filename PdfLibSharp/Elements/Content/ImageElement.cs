using PdfLibSharp.Drawing;

namespace PdfLibSharp.Elements.Content;

internal class ImageElement(IImage image) : BaseBorderElement, IImageElement
{
    public IImage Image { get; } = image;
}