using PdfLib.Drawing;

namespace PdfLib.Elements.Content;

public interface IImageElement : IElement
{
    Image Image { get; }
}