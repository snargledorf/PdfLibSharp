using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Layout;
using PdfLibSharp.Rendering;

namespace PdfLibSharp;

public sealed class PdfRenderer(IPdfBuilder pdfBuilder)
{
    public async Task RenderAsync(Pdf pdf, Stream stream, CancellationToken cancellationToken = default)
    {
        await using IPdfDocument pdfDocument = pdfBuilder.CreatePdfDocument(stream);

        foreach (IPage page in pdf.Pages)
        {
            cancellationToken.ThrowIfCancellationRequested();
                
            IPdfPage pdfPage = pdfDocument.AddPage(pdf.DefaultPageSize);

            Size pageSize = page.GetSize(pdf.DefaultPageSize.Size);
                
            using IMeasureGraphics measureGraphics = pdfPage.CreateMeasureGraphics(pageSize);
                
            var layoutBuilderFactory = new LayoutModelFactoryProvider(measureGraphics);
            var layoutScope = new LayoutScope(pdf.DefaultFont, pdf.DefaultLineHeight, pdf.DefaultStringFormat, pdf.DefaultFontColor);
                
            ILayoutModelFactory pageLayoutModelFactory = new StackLayoutModelFactory(layoutBuilderFactory);

            LayoutModel pageLayoutModel = pageLayoutModelFactory.CreateLayoutModel(page, pageSize, layoutScope);

            var renderLayoutFactoryProvider = new RenderLayoutFactoryProvider();

            IRenderLayoutFactory renderLayoutFactory = renderLayoutFactoryProvider.GetFactory(pageLayoutModel);

            var pageContentBounds = new Rectangle(Point.Zero, pageSize);
                
            RenderLayout renderPageLayout = renderLayoutFactory.CreateRenderLayout(pageLayoutModel, pageContentBounds);

            using IGraphics graphics = pdfPage.CreateGraphics();

            var renderer = new Renderer(graphics);
            renderer.Render(renderPageLayout);
        }

        await pdfDocument.SaveAsync(cancellationToken);
    }
}