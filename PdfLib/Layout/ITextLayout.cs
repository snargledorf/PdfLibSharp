using PdfLib.Drawing;

namespace PdfLib.Layout;

internal interface ITextLayout : ILayout
{
    IReadOnlyCollection<Line> Lines { get; }
    StringFormat Format { get; }
    Brush Brush { get; }
    Font Font { get; }
}