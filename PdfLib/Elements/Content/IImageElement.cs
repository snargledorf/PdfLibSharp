using PdfLib.Drawing;

namespace PdfLib.Elements.Content;

public interface IImageElement : IBorderElement
{
    Image Image { get; }
}