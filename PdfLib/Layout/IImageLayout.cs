using PdfLibrary.Drawing;

namespace PdfLibrary.Layout;

internal interface IImageLayout : ILayout
{
    Image Image { get; }
}