using PdfLibSharp.Layout;

namespace PdfLibSharp.Rendering;

internal interface IRenderer
{
    void Render(PositionedLayout layout);
}