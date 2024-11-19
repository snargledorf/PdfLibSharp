using PdfLibSharp.Elements.Layout;
using PdfLibSharp.Drawing;

namespace PdfLibSharp.Elements;

public interface IPage : IStackContainer
{
    Pdf Pdf { get; }
}