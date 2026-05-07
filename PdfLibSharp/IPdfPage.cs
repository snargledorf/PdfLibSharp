using PdfLibSharp.Drawing;

namespace PdfLibSharp;

public interface IPdfPage : IDisposable
{
    IGraphics CreateGraphics();
    IMeasureGraphics CreateMeasureGraphics(Size size);
}