namespace PdfLibrary.Drawing;

public interface IMeasureGraphics : IDisposable
{
    Size MeasureString(string text, Font font);
}