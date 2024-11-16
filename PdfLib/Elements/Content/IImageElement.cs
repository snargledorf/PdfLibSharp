using PdfLibrary.Drawing;

namespace PdfLibrary.Elements.Content;

public interface IImageElement : IElement
{
    Image Image { get; }
}