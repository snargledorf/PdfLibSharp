using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal interface ILayoutModelFactory
{
    LayoutModel CreateLayoutModel(IElement element, Size constraints, LayoutScope scope);
}