using PdfLib.Drawing;

namespace PdfLib.Elements;

public interface IBorder
{
    Color? BorderColor { get; set; }
    Dimension? BorderWidth { get; set; }
}