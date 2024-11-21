using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal record StackRenderModel(
    IStackContainer StackContainer,
    Size Size,
    Pen? BorderPen,
    IReadOnlyList<LayoutModel> ChildLayoutModels)
    : ContainerContentModel(Size, BorderPen, ChildLayoutModels);