using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record ContainerContent(IReadOnlyList<PositionedLayout> Children, Pen? BorderPen) : BorderedContent(BorderPen);