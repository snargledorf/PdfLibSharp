using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal interface ILayout
{
    Rectangle OuterBounds { get; }
    Rectangle ContentBounds { get; }
}