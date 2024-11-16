using PdfLib.Drawing;
using PdfLib.Elements;

namespace PdfLib;

public static class SizeExtensions
{
    public static Size GetSize(this ISize sizeElement, Size baseline)
    {
        Size size = baseline;

        if (sizeElement.Width.HasValue)
            size = size with { Width = sizeElement.Width.Value };
        if (sizeElement.Height.HasValue)
            size = size with { Height = sizeElement.Height.Value };
        
        return size;
    }
}