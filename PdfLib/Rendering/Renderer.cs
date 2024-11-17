using PdfLib.Drawing;
using PdfLib.Layout;
using PdfLib.Elements;
using PdfLib.Elements.Content;

namespace PdfLib.Rendering;

internal class Renderer(IGraphics graphics) : IRenderer
{
    public void Render(ILayout layout)
    {
        switch (layout)
        {
            case ITextLayout textLayout:
                RenderText(textLayout);
                break;
            case IImageLayout imageLayout:
                RenderImage(imageLayout);
                break;
            case IContainerLayout containerLayout:
                RenderContainer(containerLayout);
                break;
            case ILineLayout lineLayout:
                RenderLine(lineLayout);
                break;
        }

        if (layout is IBorderLayout { BorderPen: { } borderPen })
            RenderBorder(layout.ContentBounds, borderPen);
    }

    private void RenderLine(ILineLayout lineLayout)
    {
        graphics.DrawLine(lineLayout.Pen, lineLayout.Start, lineLayout.End);
    }

    private void RenderBorder(Rectangle rect, Pen borderPen)
    {
        graphics.DrawRectangle(borderPen, rect);
    }

    private void RenderText(ITextLayout textLayout)
    {
        Rectangle bounds = textLayout.ContentBounds;
        
        if (textLayout.Format == StringFormat.BaseLineLeft)
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

        graphics.DrawString(textLayout.Text, textLayout.Font, textLayout.Brush, bounds, textLayout.Format);
    }

    private void RenderImage(IImageLayout imageLayout)
    {
        graphics.DrawImage(imageLayout.Image, imageLayout.ContentBounds);
    }

    private void RenderContainer(IContainerLayout containerLayout)
    {
        foreach (ILayout childLayout in containerLayout.Children)
            Render(childLayout);
    }
}