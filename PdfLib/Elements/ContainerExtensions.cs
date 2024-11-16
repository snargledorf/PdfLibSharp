using PdfLib.Drawing;
using PdfLib.Elements.Content;
using PdfLib.Elements.Layout;

namespace PdfLib.Elements;

public static class ContainerExtensions
{
    public static IImageElement AddImage(this IContainer container, string filePath)
    {
        var imageContent = new ImageElement(Image.FromFile(filePath));
        container.Add(imageContent);
        return imageContent;
    }

    public static ITextElement AddText(this IContainer container, string text)
    {
        var textContent = new TextElement(text);
        container.Add(textContent);
        return textContent;
    }

    public static IStackContainer AddStack(this IContainer container, Direction direction)
    {
        var stack = new StackContainer(direction);
        container.Add(stack);
        return stack;
    }
}