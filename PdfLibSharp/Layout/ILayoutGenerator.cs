using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal interface ILayoutGenerator
{
    ILayout GenerateLayout(IElement element, Rectangle bounds, LayoutScope scope);
}