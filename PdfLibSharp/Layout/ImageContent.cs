using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record ImageContent(IImage Image, Rectangle Bounds, Pen? BorderPen) 
    : BorderedContent(Bounds, BorderPen);