using PdfLibSharp.Drawing;

namespace PdfLibSharp.Elements;

internal abstract class BaseBorderElement : BaseElement, IBorderElement
{
    public Color? BorderColor { get; set; }
    public Dimension? BorderWidth { get; set; }
}