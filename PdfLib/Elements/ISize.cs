using PdfLib.Drawing;

namespace PdfLib.Elements;

public interface ISize
{
    Dimension? Width { get; set; }
    Dimension? Height { get; set; }
}