using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;
using PdfLibSharp.Layout;

namespace PdfLibSharp.Tests;

public class StackLayoutFactoryTests
{
    private IMeasureGraphics _measureGraphics;
    private LayoutScope _layoutScope;
    private LayoutFactoryFactory _layoutFactoryFactory;

    [SetUp]
    public void Setup()
    {
        _measureGraphics = Graphics.ForMeasure(PageSize.Letter.Size);
        _layoutScope = new LayoutScope(new Font("Times New Roman", 12), 1.2, StringFormat.BaseLineLeft, Color.Black);
        _layoutFactoryFactory = new LayoutFactoryFactory(_measureGraphics);
    }

    [TearDown]
    public void Teardown()
    {
        _measureGraphics.Dispose();
        _layoutFactoryFactory.Dispose();
    }

    [Test]
    public void CreateLayoutFactory()
    {
        var stackElement = new StackContainer(Direction.Vertical);
        ILayoutFactory layoutFactory = stackElement.CreateLayoutFactory(_layoutScope, _layoutFactoryFactory);
        
        Assert.That(layoutFactory, Is.InstanceOf<StackLayoutFactory>());
        
        Assert.Multiple(() =>
        {
            Assert.That(layoutFactory.Element, Is.EqualTo(stackElement));
        });
    }
}