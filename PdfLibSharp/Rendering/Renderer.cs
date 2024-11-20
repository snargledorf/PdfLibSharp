using PdfLibSharp.Drawing;
using PdfLibSharp.Layout;

namespace PdfLibSharp.Rendering;

internal class Renderer(IGraphics graphics) : IRenderer
{
    public void Render(PositionedLayout layout)
    {
        RenderPrimaryLayout(layout);

        if (layout is BorderPositionedLayout { BorderPen: { } borderPen })
            RenderBorder(layout.ContentBounds, borderPen);
    }

    private void RenderPrimaryLayout(PositionedLayout layout)
    {
        switch (layout)
        {
            case TextPositionedLayout textLayout:
                RenderText(textLayout);
                break;
            case ImagePositionedLayout imageLayout:
                RenderImage(imageLayout);
                break;
            case ContainerPositionedLayout containerLayout:
                RenderContainer(containerLayout);
                break;
            case LinePositionedLayout lineLayout:
                RenderLine(lineLayout);
                break;
        }
    }

    private void RenderLine(LinePositionedLayout lineLayout)
    {
        graphics.DrawLine(lineLayout.Pen, lineLayout.Start, lineLayout.End);
    }

    private void RenderBorder(Rectangle contentBounds, Pen borderPen)
    {
        graphics.DrawRectangle(borderPen, contentBounds);
    }

    private void RenderText(TextPositionedLayout iTextSizedLayout)
    {
        foreach (TextLinePositionedLayout line in iTextSizedLayout.Lines)
        {
            Rectangle bounds = line.ContentBounds;
        
            if (iTextSizedLayout.Format == StringFormat.BaseLineLeft)
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
            
            graphics.DrawString(line.Text, iTextSizedLayout.Font, iTextSizedLayout.Brush, bounds, iTextSizedLayout.Format);   
        }
    }

    private void RenderImage(ImagePositionedLayout imageLayout)
    {
        graphics.DrawImage(imageLayout.Image, imageLayout.ContentBounds);
    }

    private void RenderContainer(ContainerPositionedLayout containerLayout)
    {
        foreach (PositionedLayout childLayout in containerLayout.Children)
            Render(childLayout);
    }
}