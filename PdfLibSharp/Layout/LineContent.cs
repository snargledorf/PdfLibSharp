using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record LineContent(Rectangle Bounds, Point Start, Point End, Pen Pen) : Content(Bounds);