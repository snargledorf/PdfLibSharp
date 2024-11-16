using PdfLibrary.Layout;

namespace PdfLibrary.Rendering;

internal interface IRenderer
{
    void Render(ILayout layout);
}