using PdfLib.Drawing;
using PdfLib.Elements;
using PdfLib.Layout;
using PdfSharp.Pdf;

namespace PdfLib.Rendering;

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
                pdfPage.Size = pdf.DefaultPageSize;

                Size pageSize = page.GetSize(pdf.DefaultPageSize.Size);
                
                using IMeasureGraphics measureGraphics = Graphics.ForMeasure(pageSize);
                
                var layoutGenerator = new LayoutGenerator(measureGraphics);
                var layoutScope = new LayoutScope(pdf.DefaultFont, pdf.DefaultStringFormat, pdf.DefaultFontColor);
                ILayout pageLayout = layoutGenerator.GenerateLayout(page, new Rectangle(Point.Zero, pageSize), layoutScope);

                using IGraphics graphics = Graphics.FromPdfPage(pdfPage);

                var renderer = new Renderer(graphics);
                renderer.Render(pageLayout);
            }

            pdfDocument.Save(stream);
        }, cancellationToken);
    }
}