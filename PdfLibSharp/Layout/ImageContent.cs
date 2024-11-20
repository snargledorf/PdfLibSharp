using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record ImageContent(Image Image, Pen? BorderPen) : BorderedContent(BorderPen);