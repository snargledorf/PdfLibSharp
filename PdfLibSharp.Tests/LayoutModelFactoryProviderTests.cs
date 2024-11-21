using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements.Layout;
using PdfLibSharp.Layout;

namespace PdfLibSharp.Tests;

public class LayoutModelFactoryProviderTests
{
    private IMeasureGraphics _measureGraphics;

    [SetUp]
    public void Setup()
    {
        _measureGraphics = Graphics.ForMeasure(PageSize.Letter.Size);
    }

    [TearDown]
    public void Teardown()
    {
        _measureGraphics.Dispose();
    }

    [Test]
    public void LayoutBuilderFactory_TextLayoutModelFactory()
    {
        var layoutBuilderFactory = new LayoutModelFactoryProvider(_measureGraphics);
        ILayoutModelFactory layoutModelFactory = layoutBuilderFactory.GetFactory<TextElement>();

        Assert.That(layoutModelFactory, Is.InstanceOf<TextLayoutModelFactory>());
    }

    [Test]
    public void LayoutBuilderFactory_ImageLayoutModelFactory()
    {
        var layoutBuilderFactory = new LayoutModelFactoryProvider(_measureGraphics);
        ILayoutModelFactory layoutModelFactory = layoutBuilderFactory.GetFactory<ImageElement>();

        Assert.That(layoutModelFactory, Is.InstanceOf<ImageLayoutModelFactory>());
    }

    [Test]
    public void GetStackLayoutModelFactory()
    {
        using var layoutModelFactoryProvider = new LayoutModelFactoryProvider(_measureGraphics);
        ILayoutModelFactory layoutModelFactory = layoutModelFactoryProvider.GetFactory<StackContainer>();
        Assert.That(layoutModelFactory, Is.InstanceOf<StackLayoutModelFactory>());
    }

    [Test]
    public void LayoutBuilderFactory_Dispose()
    {
        var layoutBuilderFactory = new LayoutModelFactoryProvider(_measureGraphics);
        layoutBuilderFactory.Dispose();
    }
}