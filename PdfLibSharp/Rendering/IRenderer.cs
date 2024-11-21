using PdfLibSharp.Layout;

namespace PdfLibSharp.Rendering;

internal interface IRenderer
{
    void Render(RenderLayout layout);
}