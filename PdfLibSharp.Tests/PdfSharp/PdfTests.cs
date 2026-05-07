using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;
using PdfLibSharp.PdfSharp;

namespace PdfLibSharp.Tests.PdfSharp;

public class PdfTests
{
    [Test]
    public async Task PdfSharp_BasicPdfRender()
    {
        var pdf = new Pdf(PdfSharpDrawingLoaders.Instance);

        IPage page = pdf.AddPage();

        page.AddText("Hello World");

        var memoryStream = new MemoryStream();
        var renderer = new PdfRenderer(PdfBuilder.Instance);
        await renderer.RenderAsync(pdf, memoryStream);
        
        Assert.That(memoryStream.Length, Is.GreaterThan(0));
    }

    [Test]
    public async Task PdfSharp_SubStackRender()
    {
        var pdf = new Pdf(PdfSharpDrawingLoaders.Instance);

        IPage page = pdf.AddPage();

        IStackContainer stackContainer = page.AddStack(Direction.Horizontal);
        
        stackContainer.AddText("Hello World");

        var memoryStream = new MemoryStream();
        var renderer = new PdfRenderer(PdfBuilder.Instance);
        await renderer.RenderAsync(pdf, memoryStream);
        
        Assert.That(memoryStream.Length, Is.GreaterThan(0));
    }

    [Test]
    public async Task PdfSharp_ComplexPdf()
    {
        Pdf pdf = ComplexPdfBuilder.GeneratePdf(PdfSharpDrawingLoaders.Instance);

        var memoryStream = new MemoryStream();
        var renderer = new PdfRenderer(PdfBuilder.Instance);
        await renderer.RenderAsync(pdf, memoryStream);
        
        Assert.That(memoryStream.Length, Is.GreaterThan(0));
    }
}