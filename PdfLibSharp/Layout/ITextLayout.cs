using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal interface ITextLayout : ILayout
{
    IReadOnlyCollection<TextLineLayout> Lines { get; }
    StringFormat Format { get; }
    Brush Brush { get; }
    Font Font { get; }
}