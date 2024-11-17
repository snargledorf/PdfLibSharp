using PdfLib.Drawing;

namespace PdfLib.Layout;

internal interface IBorderLayout : ILayout
{
    Pen? BorderPen { get; }
}