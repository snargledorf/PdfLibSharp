using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Layout;

namespace PdfLibSharp.Tests;

public class LayoutBuilderFactoryTests
{
    private IMeasureGraphics _measureGraphics;
    private LayoutScope _layoutScope;

    [SetUp]
    public void Setup()
    {
        _measureGraphics = Graphics.ForMeasure(PageSize.Letter.Size);
        _layoutScope = new LayoutScope(new Font("Times New Roman", 12), 1.2, StringFormat.BaseLineLeft, Color.Black);
    }

    [TearDown]
    public void Teardown()
    {
        _measureGraphics.Dispose();
    }

    [Test]
    public void LayoutBuilderFactory_TextLayoutBuilder()
    {
        var textElement = new TextElement("Hello World");
        
        var layoutBuilderFactory = new LayoutBuilderFactory(_measureGraphics);
        ILayoutBuilder layoutBuilder = layoutBuilderFactory.GetLayoutBuilder(textElement, _layoutScope);
        
        Assert.That(layoutBuilder, Is.InstanceOf<TextLayoutBuilder>());
        Assert.That(layoutBuilder.Element, Is.EqualTo(textElement)); 
    }

    [Test]
    public void LayoutBuilderFactory_ImageLayoutBuilder()
    {
        byte[] imageBytes = Convert.FromBase64String(Resources.PdfIcon);
        using var ms = new MemoryStream(imageBytes, 0, imageBytes.Length, false, true);
        
        var imageElement = new ImageElement(Image.FromStream(ms));
        
        var layoutBuilderFactory = new LayoutBuilderFactory(_measureGraphics);
        ILayoutBuilder layoutBuilder = layoutBuilderFactory.GetLayoutBuilder(imageElement, _layoutScope);
        
        Assert.That(layoutBuilder, Is.InstanceOf<ImageLayoutBuilder>());
        Assert.That(layoutBuilder.Element, Is.EqualTo(imageElement)); 
    }

    [Test]
    public void LayoutBuilderFactory_Dispose()
    {
        var layoutBuilderFactory = new LayoutBuilderFactory(_measureGraphics);
        layoutBuilderFactory.Dispose();
    }
}