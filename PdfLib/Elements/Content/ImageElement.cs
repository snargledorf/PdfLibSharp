using PdfLibrary.Drawing;

namespace PdfLibrary.Elements.Content;

internal class ImageElement(Image image) : BaseElement, IImageElement
{
    public Image Image { get; } = image;
}