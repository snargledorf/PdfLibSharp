using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Layout;

namespace PdfLibSharp.Tests;

public class LayoutFactoryFactoryTests
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
        
        var layoutBuilderFactory = new LayoutFactoryFactory(_measureGraphics);
        ILayoutFactory layoutFactory = layoutBuilderFactory.CreateLayoutFactory(textElement, _layoutScope);
        
        Assert.That(layoutFactory, Is.InstanceOf<TextLayoutFactory>());
        Assert.That(layoutFactory.Element, Is.EqualTo(textElement)); 
    }

    [Test]
    public void LayoutBuilderFactory_ImageLayoutBuilder()
    {
        byte[] imageBytes = Convert.FromBase64String(Resources.PdfIcon);
        using var ms = new MemoryStream(imageBytes, 0, imageBytes.Length, false, true);
        
        var imageElement = new ImageElement(Image.FromStream(ms));
        
        var layoutBuilderFactory = new LayoutFactoryFactory(_measureGraphics);
        ILayoutFactory layoutFactory = layoutBuilderFactory.CreateLayoutFactory(imageElement, _layoutScope);
        
        Assert.That(layoutFactory, Is.InstanceOf<ImageLayoutFactory>());
        Assert.That(layoutFactory.Element, Is.EqualTo(imageElement)); 
    }

    [Test]
    public void LayoutBuilderFactory_Dispose()
    {
        var layoutBuilderFactory = new LayoutFactoryFactory(_measureGraphics);
        layoutBuilderFactory.Dispose();
    }
}