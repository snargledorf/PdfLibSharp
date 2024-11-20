using PdfLibSharp.Elements;
using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal interface ILayoutBuilderFactory : IDisposable
{
    ILayoutFactory CreateLayoutFactory(IElement element, LayoutScope scope);
}