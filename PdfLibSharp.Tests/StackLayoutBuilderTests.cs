using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;
using PdfLibSharp.Layout;

namespace PdfLibSharp.Tests;

public class StackLayoutBuilderTests
{
    private IMeasureGraphics _measureGraphics;
    private LayoutScope _layoutScope;
    private LayoutBuilderFactory _layoutBuilderFactory;

    [SetUp]
    public void Setup()
    {
        _measureGraphics = Graphics.ForMeasure(PageSize.Letter.Size);
        _layoutScope = new LayoutScope(new Font("Times New Roman", 12), 1.2, StringFormat.BaseLineLeft, Color.Black);
        _layoutBuilderFactory = new LayoutBuilderFactory(_measureGraphics);
    }

    [TearDown]
    public void Teardown()
    {
        _measureGraphics.Dispose();
        _layoutBuilderFactory.Dispose();
    }

    [Test]
    public void GetLayoutBuilder()
    {
        var stackElement = new StackContainer(Direction.Vertical);
        ILayoutBuilder layoutBuilder = stackElement.GetLayoutBuilder(_layoutScope, _layoutBuilderFactory);
        
        Assert.That(layoutBuilder, Is.InstanceOf<StackLayoutBuilder>());
        
        Assert.Multiple(() =>
        {
            Assert.That(layoutBuilder.Element, Is.EqualTo(stackElement));
            Assert.That(layoutBuilder.ContentSize, Is.EqualTo(new Size()));
            Assert.That(layoutBuilder.OuterSize, Is.EqualTo(new Size()));
        });
    }
}