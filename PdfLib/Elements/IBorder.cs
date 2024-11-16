using PdfLibrary.Drawing;

namespace PdfLibrary.Elements;

public interface IBorder
{
    Color? BorderColor { get; set; }
    Dimension? BorderWidth { get; set; }
}