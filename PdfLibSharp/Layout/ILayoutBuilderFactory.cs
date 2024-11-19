using PdfLibSharp.Elements;
using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal interface ILayoutBuilderFactory : IDisposable
{
    ILayoutBuilder GetLayoutBuilder(IElement element, LayoutScope scope);
}