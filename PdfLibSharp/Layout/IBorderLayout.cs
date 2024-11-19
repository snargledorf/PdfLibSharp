using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal interface IBorderLayout : ILayout
{
    Pen? BorderPen { get; }
}