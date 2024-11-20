using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal abstract class ElementLayout(IElement element, Size contentSize) : ILayout
{
    public ElementSizing Sizing { get; } = element.Sizing;
    public Margins Margins { get; } = element.Margins;
    public Size ContentSize { get; } = contentSize;

    public PositionedLayout ToPositionedLayout(Rectangle outerBounds)
    {
        Size innerBoundsSize = outerBounds.Size - Margins.ToSize();
        
        var contentBounds = new Rectangle
        (
            Point: outerBounds.Point + new Point(Margins.Left, Margins.Top),
            Size: element.GetSize(innerBoundsSize)
        );
        
        object content = BuildContent(contentBounds);
        
        return new PositionedLayout(contentBounds, outerBounds, content);
    }

    protected abstract object BuildContent(Rectangle contentBounds);
}