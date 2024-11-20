using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements.Layout;
using PdfLibSharp.Layout;

namespace PdfLibSharp.Tests;

public class StackLayoutTests
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
    public void TextElementExpandsToFillHeightInVerticalStack()
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

        var positionedLayout = layout.ToPositionedLayout(new Rectangle(Point.Zero, PageSize.Letter.Size));

        // Assert
        Dimension expectedTextLayoutContentWidth = ((StackLayout)layout).ChildLayouts[0].ContentSize.Width;
        Size stackPositionedLayoutContentSize = positionedLayout.ContentBounds.Size;
        
        var containerContent = positionedLayout.ContentAs<ContainerContent>();
        PositionedLayout containerChild = containerContent.Children[0];

        Size childOuterSize = containerChild.OuterBounds.Size;
       
        Assert.Multiple(() =>
        {
            Assert.That(childOuterSize.Width, Is.EqualTo(expectedTextLayoutContentWidth), "Child element width should be sized to content");
            Assert.That(childOuterSize.Height, Is.EqualTo(stackPositionedLayoutContentSize.Height), "Child element height should match stack height");
        });
    }
    
    [Test]
    public void TextElementExpandsToFillWidthInHorizontalStack()
    {
        // Arrange
        var stackElement = new StackContainer(Direction.Horizontal);

        var textElement = new TextElement("Sample text")
        {
            Sizing = ElementSizing.ExpandToFillBounds
        };

        stackElement.Add(textElement);

        // Act
        ILayoutFactory layoutFactory = stackElement.CreateLayoutFactory(_layoutScope, _layoutFactoryFactory);

        ILayout layout = layoutFactory.CreateLayout(PageSize.Letter.Size);

        var positionedLayout = layout.ToPositionedLayout(new Rectangle(Point.Zero, PageSize.Letter.Size));

        // Assert
        Dimension expectedTextLayoutContentHeight = ((StackLayout)layout).ChildLayouts[0].ContentSize.Height;
        Size stackPositionedLayoutContentSize = positionedLayout.ContentBounds.Size;

        var containerContent = positionedLayout.ContentAs<ContainerContent>();
        PositionedLayout containerChild = containerContent.Children[0];

        Size childOuterSize = containerChild.OuterBounds.Size;

        Assert.Multiple(() =>
        {
            Assert.That(childOuterSize.Height, Is.EqualTo(expectedTextLayoutContentHeight), "Child element height should be sized to content");
            Assert.That(childOuterSize.Width, Is.EqualTo(stackPositionedLayoutContentSize.Width), "Child element width should match stack width");
        });
    }
}