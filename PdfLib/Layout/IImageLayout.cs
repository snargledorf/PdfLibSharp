using PdfLib.Drawing;

namespace PdfLib.Layout;

internal interface IImageLayout : ILayout
{
    Image Image { get; }
}