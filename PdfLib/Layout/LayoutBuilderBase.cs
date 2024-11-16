using PdfLibrary.Drawing;
using PdfLibrary.Elements;

namespace PdfLibrary.Layout;

internal abstract class LayoutBuilderBase(IElement element, Size contentSize)
{
    public Size OuterSize { get; } = new
    (
        Width: contentSize.Width + element.Margins.Left + element.Margins.Right,
        Height: contentSize.Height + element.Margins.Top + element.Margins.Bottom
    );

    public Size ContentSize { get; } = contentSize;
    public IElement Element { get; } = element;
    
    public abstract ILayout BuildLayout(Rectangle bounds);
}