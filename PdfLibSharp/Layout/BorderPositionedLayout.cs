using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record BorderedContent(Rectangle Bounds, Pen? BorderPen) : Content(Bounds);