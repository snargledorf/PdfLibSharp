using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal abstract class ElementLayout(IElement element, Size contentSize) : ILayout
{
    public Margins Margins { get; } = element.Margins;
    
    public Size ContentSize { get; } = contentSize;
    
    public abstract PositionedLayout ToPositionedLayout(Rectangle contentBounds);
}