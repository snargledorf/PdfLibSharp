namespace PdfLibSharp.Layout;

internal class RenderLayoutFactoryProvider : IRenderLayoutFactoryProvider
{
    private readonly Dictionary<Type, IRenderLayoutFactory> _renderLayoutFactories;

    public RenderLayoutFactoryProvider()
    {
        _renderLayoutFactories = new Dictionary<Type, IRenderLayoutFactory>
        {
            { typeof(TextContentModel), new TextRenderLayoutFactory() },
            { typeof(ImageContentModel), new ImageRenderLayoutFactory() },
            { typeof(LineContentModel), new LineRenderLayoutFactory() },
            { typeof(StackRenderModel), new StackRenderLayoutFactory(this) }
        };
    }

    public IRenderLayoutFactory GetFactory(LayoutModel layoutModel) => GetFactory(layoutModel.ContentModel.GetType());

    public IRenderLayoutFactory GetFactory<TContentModel>(TContentModel contentModel)
        where TContentModel : ContentModel => GetFactory(typeof(TContentModel));

    public IRenderLayoutFactory GetFactory(Type contentModelType)
    {
        if (_renderLayoutFactories.TryGetValue(contentModelType, out IRenderLayoutFactory? renderLayoutFactory))
            return renderLayoutFactory;
        
        throw new InvalidOperationException($"Unsupported type: {contentModelType}");
    }
}