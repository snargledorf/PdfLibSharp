using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements.Layout;
using PdfLibSharp.Layout;

namespace PdfLibSharp.Tests;

public class StackLayoutModelFactoryTests
{
    private static readonly Size LayoutSizeConstraints = PageSize.Letter.Size;
    
    private IMeasureGraphics _measureGraphics;
    private LayoutScope _layoutScope;
    private LayoutModelFactoryProvider _layoutModelFactoryProvider;
    
    [SetUp]
    public void Setup()
    {
        _measureGraphics = Graphics.ForMeasure(LayoutSizeConstraints);
        _layoutScope = new LayoutScope(new Font("Times New Roman", 12), 1.2, StringFormat.BaseLineLeft, Color.Black);
        _layoutModelFactoryProvider = new LayoutModelFactoryProvider(_measureGraphics);
    }

    [TearDown]
    public void Teardown()
    {
        _measureGraphics.Dispose();
        _layoutModelFactoryProvider.Dispose();
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
        ILayoutModelFactory layoutModelFactory = _layoutModelFactoryProvider.GetFactory<StackContainer>();
        LayoutModel layoutModel = layoutModelFactory.CreateLayoutModel(stackElement, LayoutSizeConstraints, _layoutScope);

        // Assert
        Assert.That(layoutModel.ContentModel, Is.InstanceOf<ContainerContentModel>());

        var stackLayout = (ContainerContentModel)layoutModel.ContentModel;
        Dimension expectedTextElementWidth = stackLayout.Size.Width;
        Dimension expectedTextElementHeight = stackLayout.Size.Height;

        layoutModel = stackLayout.ChildLayoutModels[0];

        Assert.That(layoutModel.ContentModel, Is.InstanceOf<TextContentModel>());

        var textLayout = (TextContentModel)layoutModel.ContentModel;
        Dimension actualTextElementWidth = textLayout.Size.Width;
        Dimension actualTextElementHeight = textLayout.Size.Height;

        Assert.Multiple(() =>
        {
            Assert.That(actualTextElementWidth, Is.EqualTo(expectedTextElementWidth));
            Assert.That(actualTextElementHeight, Is.EqualTo(expectedTextElementHeight));
        });
    }
}