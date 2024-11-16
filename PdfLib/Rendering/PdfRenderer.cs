using PdfLibrary.Drawing;
using PdfLibrary.Elements;
using PdfLibrary.Layout;
using PdfSharp.Pdf;

namespace PdfLibrary.Rendering;

public static class PdfRenderer
{
    public static Task RenderAsync(Pdf pdf, Stream stream, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            using var pdfDocument = new PdfDocument();

            foreach (IPage page in pdf.Pages)
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                PdfPage pdfPage = pdfDocument.AddPage();

                Size pageSize = page.GetSize(pdf.DefaultPageSize.Size);
                
                using IMeasureGraphics measureGraphics = Graphics.ForMeasure(pageSize);
                
                var layoutGenerator = new LayoutGenerator(measureGraphics);
                ILayout pageLayout = layoutGenerator.GenerateLayout(page, new Rectangle(Point.Zero, pageSize), pdf.DefaultFont, pdf.DefaultStringFormat);

                using IGraphics graphics = Graphics.FromPdfPage(pdfPage);

                var renderer = new Renderer(graphics);
                renderer.Render(pageLayout);
            }

            pdfDocument.Save(stream);
        }, cancellationToken);
    }
}