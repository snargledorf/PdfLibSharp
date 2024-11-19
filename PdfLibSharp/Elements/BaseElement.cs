using PdfLibSharp.Drawing;

namespace PdfLibSharp.Elements;

internal class BaseElement : IElement
{
    public Margins Margins { get; } = new();
    public ElementSizing Sizing { get; set; }
    public Dimension? Width { get; set; }
    public Dimension? Height { get; set; }
}