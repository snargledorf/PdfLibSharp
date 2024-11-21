using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record ContainerContent(Rectangle Bounds, Pen? BorderPen, IReadOnlyList<RenderLayout> Children) 
    : BorderedContent(Bounds, BorderPen);