using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
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

        Assert.Multiple(() => { Assert.That(layoutFactory.Element, Is.EqualTo(stackElement)); });
    }


    [Test]
    public void TextElementExpandsToFillWidthInVerticalStack()
    {
        // Arrange
        var stackElement = new StackContainer(Direction.Vertical);
        var textElement = new TextElement("Sample text")
        {
            Sizing = ElementSizing.ExpandToFillBounds
        };

        stackElement.Add(textElement);

        // Act
        ILayoutFactory layoutFactory = stackElement.CreateLayoutFactory(_layoutScope, _layoutFactoryFactory);
        ILayout layout = layoutFactory.CreateLayout(PageSize.Letter.Size);

        // Assert
        Assert.That(layout, Is.InstanceOf<StackLayout>());

        var stackLayout = (StackLayout)layout;
        Dimension expectedTextElementWidth = stackLayout.ContentSize.Width;
        Dimension expectedTextElementHeight = stackLayout.ContentSize.Height;

        layout = stackLayout.ChildLayouts[0];

        Assert.That(layout, Is.InstanceOf<TextLayout>());

        var textLayout = (TextLayout)layout;
        Dimension actualTextElementWidth = textLayout.ContentSize.Width;
        Dimension actualTextElementHeight = textLayout.ContentSize.Height;

        Assert.Multiple(() =>
        {
            Assert.That(actualTextElementWidth, Is.EqualTo(expectedTextElementWidth));
            Assert.That(actualTextElementHeight, Is.EqualTo(expectedTextElementHeight));
        });
    }
}