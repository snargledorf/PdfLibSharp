using PdfLib.Drawing;

namespace PdfLib.Elements.Content;

internal class ImageElement(Image image) : BaseBorderElement, IImageElement
{
    public Image Image { get; } = image;
}