using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal abstract record ContainerContentModel(
    Size Size,
    Pen? BorderPen,
    IReadOnlyList<LayoutModel> ChildLayoutModels) : BorderElementContentModel(Size, BorderPen)
{
    public IReadOnlyCollection<LayoutModel> ContentSizedChildModels =>
        ChildLayoutModels.Where(model => model.Sizing == ElementSizing.Content).ToArray();
    
    public int FillElementsCount => ChildLayoutModels.Count(model => model.Sizing == ElementSizing.ExpandToFillBounds);
}