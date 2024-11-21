using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal abstract record BorderElementContentModel(Size Size, Pen? BorderPen) : ContentModel(Size);