using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements.Layout;
using PdfLibSharp.Layout;

namespace PdfLibSharp.Tests;

public class StackRenderModelTests
{
    private IMeasureGraphics _measureGraphics;
    private LayoutScope _layoutScope;
    private LayoutModelFactoryProvider _layoutModelFactoryProvider;
    private RenderLayoutFactoryProvider _renderLayoutFactoryProvider;

    [SetUp]
    public void Setup()
    {
        _measureGraphics = Graphics.ForMeasure(PageSize.Letter.Size);
        _layoutScope = new LayoutScope(new Font("Times New Roman", 12), 1.2, StringFormat.BaseLineLeft, Color.Black);
        _layoutModelFactoryProvider = new LayoutModelFactoryProvider(_measureGraphics);
        _renderLayoutFactoryProvider = new RenderLayoutFactoryProvider();
    }

    [TearDown]
    public void Teardown()
    {
        _measureGraphics.Dispose();
        _layoutModelFactoryProvider.Dispose();
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
        var layoutModelFactory = new StackLayoutModelFactory(_layoutModelFactoryProvider);
        
        LayoutModel layoutModel = layoutModelFactory.CreateLayoutModel(stackElement, PageSize.Letter.Size, _layoutScope);

        var renderLayoutFactory = new StackRenderLayoutFactory(_renderLayoutFactoryProvider);

        RenderLayout renderLayout = renderLayoutFactory.CreateRenderLayout(layoutModel, new Rectangle(Point.Zero, PageSize.Letter.Size));

        // Assert
        Dimension expectedTextLayoutContentWidth = ((StackRenderModel)layoutModel.ContentModel).ChildLayoutModels[0].ContentModel.Size.Width;
        Size stackPositionedLayoutContentSize = renderLayout.Content.Bounds.Size;
        
        var containerContent = renderLayout.ContentAs<ContainerContent>();
        RenderLayout containerChild = containerContent.Children[0];

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
        var layoutModelFactory = new StackLayoutModelFactory(_layoutModelFactoryProvider);
        
        LayoutModel layoutModel = layoutModelFactory.CreateLayoutModel(stackElement, PageSize.Letter.Size, _layoutScope);

        var renderLayoutFactory = new StackRenderLayoutFactory(_renderLayoutFactoryProvider);

        RenderLayout renderLayout = renderLayoutFactory.CreateRenderLayout(layoutModel, new Rectangle(Point.Zero, PageSize.Letter.Size));

        // Assert
        Dimension expectedTextLayoutContentHeight = ((StackRenderModel)layoutModel.ContentModel).ChildLayoutModels[0].ContentModel.Size.Height;
        Size stackPositionedLayoutContentSize = renderLayout.Content.Bounds.Size;

        var containerContent = renderLayout.ContentAs<ContainerContent>();
        RenderLayout containerChild = containerContent.Children[0];

        Size childOuterSize = containerChild.OuterBounds.Size;

        Assert.Multiple(() =>
        {
            Assert.That(childOuterSize.Height, Is.EqualTo(expectedTextLayoutContentHeight), "Child element height should be sized to content");
            Assert.That(childOuterSize.Width, Is.EqualTo(stackPositionedLayoutContentSize.Width), "Child element width should match stack width");
        });
    }
}