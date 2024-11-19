namespace PdfLibSharp.Drawing;

public interface IMeasureGraphics : IDisposable
{
    Size MeasureString(string text, Font font);
}