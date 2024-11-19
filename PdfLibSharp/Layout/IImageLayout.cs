using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal interface IImageLayout : ILayout
{
    Image Image { get; }
}