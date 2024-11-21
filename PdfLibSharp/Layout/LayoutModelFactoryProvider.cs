using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class LayoutModelFactoryProvider : ILayoutModelFactoryProvider
{
    private readonly Dictionary<Type, ILayoutModelFactory> _layoutFactories;

    private readonly IMeasureGraphics _measureGraphics;

    public LayoutModelFactoryProvider(IMeasureGraphics measureGraphics)
    {
        _measureGraphics = measureGraphics;
        
        _layoutFactories = new Dictionary<Type, ILayoutModelFactory>
        {
            { typeof(TextElement), new TextLayoutModelFactory(measureGraphics) },
            { typeof(ImageElement), new ImageLayoutModelFactory() },
            { typeof(LineElement), new LineLayoutModelFactory() },
            { typeof(StackContainer), new StackLayoutModelFactory(this) },
        };
    }

    public ILayoutModelFactory GetFactory<TElement>() where TElement : IElement =>
        GetFactory(typeof(TElement));

    public ILayoutModelFactory GetFactory(Type elementType)
    {
        if (_layoutFactories.TryGetValue(elementType, out ILayoutModelFactory? layoutFactory))
            return layoutFactory;
        
        throw new InvalidOperationException($"Unsupported type: {elementType}");
    }

    public void Dispose()
    {
        _measureGraphics.Dispose();
    }
}