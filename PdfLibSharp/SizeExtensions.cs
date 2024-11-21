using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Layout;

namespace PdfLibSharp;

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
    
    internal static Size GetSize(this LayoutModel sizeElement, Size baseline)
    {
        Size size = baseline;

        if (sizeElement.Width.HasValue)
            size = size with { Width = sizeElement.Width.Value };
        if (sizeElement.Height.HasValue)
            size = size with { Height = sizeElement.Height.Value };
        
        return size;
    }
}