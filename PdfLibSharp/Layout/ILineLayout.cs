using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal interface ILineLayout : ILayout
{
    Pen Pen { get; }
}