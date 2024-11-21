using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal interface IRenderLayoutFactory
{
    RenderLayout CreateRenderLayout(LayoutModel model, Rectangle bounds);
}