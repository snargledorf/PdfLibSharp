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
                
                var layoutBuilderFactory = new LayoutFactoryFactory(measureGraphics);
                var layoutScope = new LayoutScope(pdf.DefaultFont, pdf.DefaultLineHeight, pdf.DefaultStringFormat, pdf.DefaultFontColor);
                
                ILayoutFactory pageLayoutFactory = page.CreateLayoutFactory(layoutScope, layoutBuilderFactory);

                ILayout pageLayout = pageLayoutFactory.CreateLayout(pageSize);
                var pageContentBounds = new Rectangle(new Point(pageLayout.Margins.Left, pageLayout.Margins.Top), pageLayout.ContentSize - pageLayout.Margins.ToSize());
                var positionedPageLayout = pageLayout.ToPositionedLayout(pageContentBounds);

                using IGraphics graphics = Graphics.FromPdfPage(pdfPage);

                var renderer = new Renderer(graphics);
                renderer.Render(positionedPageLayout);
            }

            pdfDocument.Save(stream);
        }, cancellationToken);
    }
}