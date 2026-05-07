using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Elements;

internal sealed class Page : StackContainer, IPage
{
    internal Page(Pdf pdf) : base(Direction.Vertical, pdf)
    {
    }
}