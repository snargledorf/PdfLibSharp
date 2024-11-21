using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal abstract class BaseLayoutModelFactory : ILayoutModelFactory
{
    private readonly Dictionary<Size, LayoutModel> _cachedLayouts = new();

    public LayoutModel CreateLayoutModel(IElement element, Size constraints, LayoutScope scope)
    {
        if (_cachedLayouts.TryGetValue(constraints, out LayoutModel? layout))
            return layout;

        Size innerConstraints = constraints - element.Margins.ToSize();
        innerConstraints = element.GetSize(innerConstraints);

        ContentModel contentModel = CreateContentModel(element, innerConstraints, scope);

        return _cachedLayouts[constraints] =
            new LayoutModel(contentModel, element.Margins, element.Sizing, element.Width, element.Height);
    }

    protected abstract ContentModel CreateContentModel(IElement element, Size constraints, LayoutScope scope);
}