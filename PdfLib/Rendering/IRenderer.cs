using PdfLib.Layout;

namespace PdfLib.Rendering;

internal interface IRenderer
{
    void Render(ILayout layout);
}