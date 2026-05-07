using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record ImageContentModel(IImage Image, Size Size, Pen? BorderPen) : BorderElementContentModel(Size, BorderPen);