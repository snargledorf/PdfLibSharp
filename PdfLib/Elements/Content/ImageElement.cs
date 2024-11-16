using PdfLib.Drawing;

namespace PdfLib.Elements.Content;

internal class ImageElement(Image image) : BaseElement, IImageElement
{
    public Image Image { get; } = image;
}