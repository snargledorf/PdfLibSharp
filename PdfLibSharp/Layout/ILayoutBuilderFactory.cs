using PdfLibSharp.Elements;
using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal interface ILayoutBuilderFactory
{
    ILayoutBuilder GetLayoutBuilder(IElement element, LayoutScope scope);
}