using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Elements;

public static class ContainerExtensions
{
    extension(IContainer container)
    {
        public IImageElement AddImage(string filePath)
        {
            var imageContent = new ImageElement(container.Pdf.ImageLoader.LoadImageFromFile(filePath));
            container.Add(imageContent);
            return imageContent;
        }

        public ITextElement AddText(string text)
        {
            var textContent = new TextElement(text);
            container.Add(textContent);
            return textContent;
        }

        public IStackContainer AddStack(Direction direction)
        {
            var stack = new StackContainer(direction, container.Pdf);
            container.Add(stack);
            return stack;
        }

        public ILineElement AddLine()
        {
            return container.AddLine(1);
        }

        public ILineElement AddLine(Dimension width)
        {
            return container.AddLine(width, Color.Black);
        }

        public ILineElement AddLine(Dimension width, Color color)
        {
            return container.AddLine(new Pen(color, width));
        }

        public ILineElement AddLine(Pen pen)
        {
            var lineElement = new LineElement(pen);
            container.Add(lineElement);
            return lineElement;
        }
    }
}