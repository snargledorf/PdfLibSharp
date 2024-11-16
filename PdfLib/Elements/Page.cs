using PdfLib.Elements.Layout;

namespace PdfLib.Elements;

internal sealed class Page : StackContainer, IPage
{
    internal Page(Pdf pdf) : base(Direction.Vertical)
    {
        Pdf = pdf;
    }

    public Pdf Pdf { get; }
}