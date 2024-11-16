using PdfLib.Drawing;

namespace PdfLib.Layout;

internal interface ITextLayout : ILayout
{
    string Text { get; }
    StringFormat Format { get; }
    Brush Brush { get; }
    Font Font { get; }
}