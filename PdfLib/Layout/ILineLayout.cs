using PdfLib.Drawing;

namespace PdfLib.Layout;

internal interface ILineLayout : ILayout
{
    Pen Pen { get; }
    Point Start { get; }
    Point End { get; }
}