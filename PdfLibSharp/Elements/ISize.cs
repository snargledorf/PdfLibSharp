using PdfLibSharp.Drawing;

namespace PdfLibSharp.Elements;

public interface ISize
{
    Dimension? Width { get; set; }
    Dimension? Height { get; set; }
}