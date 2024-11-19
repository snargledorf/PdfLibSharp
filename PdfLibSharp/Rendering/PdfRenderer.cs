using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Layout;
using PdfSharp.Pdf;

namespace PdfLibSharp.Rendering;

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
                
                var layoutBuilderFactory = new LayoutBuilderFactory(measureGraphics);
                var layoutScope = new LayoutScope(pdf.DefaultFont, pdf.DefaultLineHeight, pdf.DefaultStringFormat, pdf.DefaultFontColor);
                
                ILayoutBuilder pageLayoutBuilder = page.GetLayoutBuilder(layoutScope, layoutBuilderFactory);

                ILayout pageLayout = pageLayoutBuilder.BuildLayout(new Rectangle(Point.Zero, pageSize));

                using IGraphics graphics = Graphics.FromPdfPage(pdfPage);

                var renderer = new Renderer(graphics);
                renderer.Render(pageLayout);
            }

            pdfDocument.Save(stream);
        }, cancellationToken);
    }
}