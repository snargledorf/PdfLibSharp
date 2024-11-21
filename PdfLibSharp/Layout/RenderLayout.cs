using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal sealed record RenderLayout(Content Content, Rectangle OuterBounds);