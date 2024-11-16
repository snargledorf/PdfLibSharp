using PdfLibrary.Drawing;
using PdfLibrary.Elements;

namespace PdfLibrary.Layout;

internal sealed class ContainerLayout(Point point, Size contentSize, Margins margins, Pen? borderPen, IReadOnlyList<ILayout> children)
    : ElementLayout(point, contentSize, margins, borderPen), IContainerLayout
{
    public IReadOnlyList<ILayout> Children { get; } = children;
}