using PdfLib.Drawing;
using PdfLib.Elements.Layout;

namespace PdfLib.Elements;

public interface ILineElement : IElement, IDirection
{
    Pen Pen { get; }
}