using PdfLibSharp.Drawing.Units;

namespace PdfLibSharp;

public interface IPdfBuilder
{
    IPdfDocument CreatePdfDocument(Stream stream, int ppi = UnitPixel.DefaultPixelsPerInch);
}