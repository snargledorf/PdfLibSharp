using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal interface ILineLayout : ILayout
{
    Pen Pen { get; }
    Point Start { get; }
    Point End { get; }
}