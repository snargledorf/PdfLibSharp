namespace PdfLibSharp.Layout;

internal interface IRenderLayoutFactoryProvider
{
    IRenderLayoutFactory GetFactory(LayoutModel layoutModel);
    IRenderLayoutFactory GetFactory<TContentModel>(TContentModel contentModel) where TContentModel : ContentModel;
    IRenderLayoutFactory GetFactory(Type contentModelType);
}