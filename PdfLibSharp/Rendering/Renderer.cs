using PdfLibSharp.Drawing;
using PdfLibSharp.Layout;

namespace PdfLibSharp.Rendering;

internal class Renderer(IGraphics graphics) : IRenderer
{
    public void Render(PositionedLayout layout)
    {
        RenderPrimaryLayout(layout);

        if (layout.Content is BorderedContent { BorderPen: { } borderPen })
            RenderBorder(layout.ContentBounds, borderPen);
    }

    private void RenderPrimaryLayout(PositionedLayout layout)
    {
        switch (layout.Content)
        {
            case TextContent textContent:
                RenderText(textContent);
                break;
            case ImageContent imageContent:
                RenderImage(imageContent, layout.ContentBounds);
                break;
            case ContainerContent containerContent:
                RenderContainer(containerContent);
                break;
            case LineContent lineContent:
                RenderLine(lineContent, layout.ContentBounds);
                break;
        }
    }

    private void RenderLine(LineContent lineLayout, Rectangle contentBounds)
    {
        graphics.DrawLine(lineLayout.Pen, contentBounds.TopLeft, contentBounds.BottomRight);
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

    private void RenderImage(ImageContent imageContent, Rectangle contentBounds)
    {
        graphics.DrawImage(imageContent.Image, contentBounds);
    }

    private void RenderContainer(ContainerContent containerLayout)
    {
        foreach (PositionedLayout childLayout in containerLayout.Children)
            Render(childLayout);
    }
}