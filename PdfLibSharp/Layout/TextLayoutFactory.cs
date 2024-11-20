using System.Text;
using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class TextLayoutFactory(ITextElement textElement, LayoutScope layoutScope, IMeasureGraphics measureGraphics)
    : BorderLayoutFactory(textElement)
{
    protected override ILayout CreateInnerLayout(Size constraints)
    {
        Font font = textElement.GetFont(layoutScope.Font);
        double lineHeight = textElement.LineHeight ?? layoutScope.LineHeight;
        StringFormat stringFormat = textElement.StringFormat ?? layoutScope.StringFormat;
        Color fontColor = textElement.FontColor ?? layoutScope.FontColor;

        IReadOnlyCollection<IReadOnlyCollection<Word>> lines = TextLayoutBuilderHelpers.GetLines(textElement.Text, font, lineHeight, measureGraphics);
        
        var lineLayouts = new List<TextLine>();
        var lineBuilder = new StringBuilder();

        var lineSize = new Size();
        
        foreach (IReadOnlyCollection<Word> line in lines)
        {
            foreach (Word word in line)
            {
                if (lineSize.Width + word.Size.Width > constraints.Width)
                {
                    lineSize = lineSize with
                    {
                        Height = Math.Max(lineSize.Height, word.Size.Height)
                    };
                    
                    lineLayouts.Add(new TextLine(lineBuilder.ToString(), lineSize));
                    
                    lineSize = new Size();
                    lineBuilder.Clear();
                }

                lineSize = new Size
                (
                    Width: lineSize.Width + word.Size.Width,
                    Height: Math.Max(lineSize.Height, word.Size.Height)
                );
                
                lineBuilder.Append(word.Text);
            }
            
            lineLayouts.Add(new TextLine(lineBuilder.ToString(), lineSize));
                    
            lineSize = new Size();
            lineBuilder.Clear();
        }

        // Content size could be greater than constraints (IE. Overflow)
        // TODO: Add overflow handling/logic
        Size contentSize = lineLayouts
            .Select(ll => ll.ContentSize)
            .GetCombinedSize(Direction.Vertical);

        return new TextLayout(
            textElement,
            lineLayouts.ToArray(),
            contentSize,
            font,
            stringFormat,
            new SolidBrush(fontColor),
            BorderPen);
    }
}