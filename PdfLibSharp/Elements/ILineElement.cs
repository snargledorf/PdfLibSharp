using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Elements;

public interface ILineElement : IElement, IDirection
{
    Pen Pen { get; }
}