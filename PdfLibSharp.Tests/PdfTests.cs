using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;
using PdfLibSharp.Rendering;

namespace PdfLibSharp.Tests;

public class PdfTests
{
    [Test]
    public async Task BasicPdfRender()
    {
        var pdf = new Pdf();

        IPage page = pdf.AddPage();

        page.AddText("Hello World");

        var memoryStream = new MemoryStream();
        await PdfRenderer.RenderAsync(pdf, memoryStream);
        
        Assert.That(memoryStream.Length, Is.GreaterThan(0));
    }

    [Test]
    public async Task SubStackRender()
    {
        var pdf = new Pdf();

        IPage page = pdf.AddPage();

        IStackContainer stackContainer = page.AddStack(Direction.Horizontal);
        
        stackContainer.AddText("Hello World");

        var memoryStream = new MemoryStream();
        await PdfRenderer.RenderAsync(pdf, memoryStream);
        
        Assert.That(memoryStream.Length, Is.GreaterThan(0));
    }

    [Test]
    public async Task ComplexPdf()
    {
        Pdf pdf = ComplexPdfBuilder.GeneratePdf();

        var memoryStream = new MemoryStream();
        await PdfRenderer.RenderAsync(pdf, memoryStream);
        
        Assert.That(memoryStream.Length, Is.GreaterThan(0));
    }
}