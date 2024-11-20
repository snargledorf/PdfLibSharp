using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal abstract class ContainerLayout(IContainer container, Size contentSize, Pen? borderPen, IReadOnlyList<ILayout> childLayouts)
    : BorderElementLayout(container, contentSize, borderPen), IContainerLayout
{
    public IReadOnlyList<ILayout> ChildLayouts { get; } = childLayouts;
}