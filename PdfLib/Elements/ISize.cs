using PdfLibrary.Drawing;

namespace PdfLibrary.Elements;

public interface ISize
{
    Dimension? Width { get; set; }
    Dimension? Height { get; set; }
}