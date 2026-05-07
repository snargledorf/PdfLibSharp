using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record ImageContentModel(Image Image, Size Size, Pen? BorderPen) : BorderElementContentModel(Size, BorderPen);