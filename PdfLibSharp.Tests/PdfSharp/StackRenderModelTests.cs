using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements.Layout;
using PdfLibSharp.Layout;
using PdfLibSharp.PdfSharp;

namespace PdfLibSharp.Tests.PdfSharp;

public class StackRenderModelTests
{
    private IMeasureGraphics _measureGraphics;
    private LayoutScope _layoutScope;
    private LayoutModelFactoryProvider _layoutModelFactoryProvider;
    private RenderLayoutFactoryProvider _renderLayoutFactoryProvider;

    [SetUp]
    public void Setup()
    {
        _measureGraphics = Graphics.ForMeasure(PageSize.Letter.Size.ToXSize());
        _layoutScope = new LayoutScope(new Font("Times New Roman", 12), 1.2, StringFormat.BaseLineLeft, StandardColors.Black);
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
        var pdf = new Pdf(PdfSharpDrawingLoaders.Instance);
        var stackElement = new StackContainer(Direction.Vertical, pdf);
        
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
        Dimension expectedTextLayoutContentWidth = ((PdfSharpStackRenderModel)layoutModel.ContentModel).ChildLayoutModels[0].ContentModel.Size.Width;
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
        var pdf = new Pdf(PdfSharpDrawingLoaders.Instance);
        var stackElement = new StackContainer(Direction.Horizontal, pdf);

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
        Dimension expectedTextLayoutContentHeight = ((PdfSharpStackRenderModel)layoutModel.ContentModel).ChildLayoutModels[0].ContentModel.Size.Height;
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