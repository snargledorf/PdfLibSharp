using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal abstract class BaseLayoutModelFactory : ILayoutModelFactory
{
    private readonly Dictionary<Size, LayoutModel> _cachedLayouts = new();

    public LayoutModel CreateLayoutModel(IElement element, Size constraints, LayoutScope scope)
    {
        Size innerConstraints = constraints - element.Margins.ToSize();
        innerConstraints = element.GetSize(innerConstraints);

        ContentModel contentModel = CreateContentModel(element, innerConstraints, scope);

        return new LayoutModel(contentModel, element.Margins, element.Sizing, element.Width, element.Height);
    }

    protected abstract ContentModel CreateContentModel(IElement element, Size constraints, LayoutScope scope);
}