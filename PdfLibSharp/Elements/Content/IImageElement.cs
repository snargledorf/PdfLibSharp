using PdfLibSharp.Drawing;

namespace PdfLibSharp.Elements.Content;

public interface IImageElement : IBorderElement
{
    Image Image { get; }
}