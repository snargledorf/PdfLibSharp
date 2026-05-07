using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record ImageContent(Image Image, Rectangle Bounds, Pen? BorderPen) 
    : BorderedContent(Bounds, BorderPen);