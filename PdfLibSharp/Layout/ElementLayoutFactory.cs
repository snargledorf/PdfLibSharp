using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal abstract class ElementLayoutFactory(IElement element) : ILayoutFactory
{
    private readonly Dictionary<Size, ILayout> _cachedLayouts = new();
    
    public IElement Element { get; } = element;

    public ILayout CreateLayout(Size constraints)
    {
        if (_cachedLayouts.TryGetValue(constraints, out ILayout? layout))
            return layout;
        
        Size innerConstraints = constraints - Element.Margins.ToSize();
        innerConstraints = Element.GetSize(innerConstraints);
        
        // REVIEW: Wrap inner layout in a margin layout?
        return _cachedLayouts[constraints] = CreateInnerLayout(innerConstraints);
    }

    protected abstract ILayout CreateInnerLayout(Size constraints);
}