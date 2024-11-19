using System.Text;
using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class TextLayoutBuilder(ITextElement textElement, IReadOnlyCollection<IReadOnlyCollection<Word>> lines, Size contentSize, Font font, Color fontColor, StringFormat stringFormat)
    : BorderLayoutBuilder(textElement, contentSize)
{
    protected override ILayout BuildLayout(Rectangle bounds, Pen? borderPen)
    {
        var lineLayouts = new List<TextLineLayout>();
        var lineBuilder = new StringBuilder();

        var lineBounds = new Rectangle
        (
            Point: Element.GetInnerPoint(bounds.Point),
            Size: new Size()
        );
        
        foreach (IReadOnlyCollection<Word> line in lines)
        {
            foreach (Word word in line)
            {
                if (lineBounds.Size.Width + word.Size.Width > bounds.Size.Width)
                {
                    lineBounds = lineBounds with
                    {
                        Size = lineBounds.Size with
                        {
                            Width = bounds.Size.Width
                        }
                    };
                    
                    lineLayouts.Add(new TextLineLayout(lineBuilder.ToString(), lineBounds));
                    
                    lineBounds = new Rectangle
                    (
                        Point: bounds.Point with
                        {
                            Y = lineBounds.Point.Y + lineBounds.Size.Height
                        },
                        Size: new Size()
                    );

                    lineBuilder.Clear();
                }

                lineBounds = lineBounds with
                {
                    Size = new Size
                    (
                        Width: lineBounds.Size.Width + word.Size.Width,
                        Height: Math.Max(lineBounds.Size.Height, word.Size.Height)
                    )
                };
                
                lineBuilder.Append(word.Text);
            }
            
            lineBounds = lineBounds with
            {
                Size = lineBounds.Size with
                {
                    Width = bounds.Size.Width
                }
            };
            
            lineLayouts.Add(new TextLineLayout(lineBuilder.ToString(), lineBounds));
                    
            lineBounds = new Rectangle
            (
                Point: bounds.Point with
                {
                    Y = lineBounds.Point.Y + lineBounds.Size.Height
                },
                Size: new Size()
            );

            lineBuilder.Clear();
        }

        Size layoutSize = lineLayouts
            .Select(ll => ll.OuterBounds.Size)
            .GetCombinedSize(Direction.Vertical);

        if (Element.Sizing == ElementSizing.ExpandToFillBounds)
        {
            Size innerBounds = Element.GetInnerSize(bounds.Size);
            layoutSize = new Size
            (
                Width: Math.Max(layoutSize.Width, innerBounds.Width),
                Height: Math.Max(layoutSize.Height, innerBounds.Height)
            );
        }

        return new TextLayout(
            lineLayouts.ToArray(),
            bounds.Point,
            layoutSize,
            textElement.Margins,
            textElement.GetFont(font),
            textElement.StringFormat ?? stringFormat,
            new SolidBrush(textElement.FontColor ?? fontColor),
            borderPen);
    }
}