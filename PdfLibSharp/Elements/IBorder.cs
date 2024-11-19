using PdfLibSharp.Drawing;

namespace PdfLibSharp.Elements;

public interface IBorder
{
    Color? BorderColor { get; set; }
    Dimension? BorderWidth { get; set; }
}