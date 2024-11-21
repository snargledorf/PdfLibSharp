using PdfLibSharp.Drawing;
using PdfLibSharp.Layout;

namespace PdfLibSharp.Rendering;

internal class Renderer(IGraphics graphics) : IRenderer
{
    public void Render(RenderLayout layout)
    {
        RenderPrimaryLayout(layout);

        if (layout.Content is BorderedContent { BorderPen: { } borderPen } borderedContent)
            RenderBorder(borderedContent.Bounds, borderPen);
    }

    private void RenderPrimaryLayout(RenderLayout layout)
    {
        switch (layout.Content)
        {
            case TextContent textContent:
                RenderText(textContent);
                break;
            case ImageContent imageContent:
                RenderImage(imageContent);
                break;
            case ContainerContent containerContent:
                RenderContainer(containerContent);
                break;
            case LineContent lineContent:
                RenderLine(lineContent);
                break;
        }
    }

    private void RenderLine(LineContent lineContent)
    {
        graphics.DrawLine(lineContent.Pen, lineContent.Bounds.TopLeft, lineContent.Bounds.BottomRight);
    }

    private void RenderBorder(Rectangle contentBounds, Pen borderPen)
    {
        graphics.DrawRectangle(borderPen, contentBounds);
    }

    private void RenderText(TextContent textContent)
    {
        foreach (PositionedTextLine line in textContent.Lines)
        {
            Rectangle bounds = line.ContentBounds;
        
            if (textContent.Format == StringFormat.BaseLineLeft)
            {
                // BaseLineLeft strings need to have a height of 0
                bounds = bounds with
                {
                    Size = bounds.Size with
                    {
                        Height = 0
                    }
                };
            }
            
            graphics.DrawString(line.Text, textContent.Font, textContent.Brush, bounds, textContent.Format);   
        }
    }

    private void RenderImage(ImageContent imageContent)
    {
        graphics.DrawImage(imageContent.Image, imageContent.Bounds);
    }

    private void RenderContainer(ContainerContent containerContent)
    {
        foreach (RenderLayout childLayout in containerContent.Children)
            Render(childLayout);
    }
}