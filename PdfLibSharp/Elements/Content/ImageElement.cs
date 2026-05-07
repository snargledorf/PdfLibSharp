using PdfLibSharp.Drawing;

namespace PdfLibSharp.Elements.Content;

internal class ImageElement(Image image) : BaseBorderElement, IImageElement
{
    public Image Image { get; } = image;
}