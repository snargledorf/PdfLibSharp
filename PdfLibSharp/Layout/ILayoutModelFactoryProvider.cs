using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal interface ILayoutModelFactoryProvider : IDisposable
{
    ILayoutModelFactory GetFactory<TElement>() where TElement : IElement;
    ILayoutModelFactory GetFactory(Type elementType);
}