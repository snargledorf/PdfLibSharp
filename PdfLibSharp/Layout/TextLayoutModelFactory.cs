using System.Text;
using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class TextLayoutModelFactory(IMeasureGraphics measureGraphics)
    : BorderLayoutModelFactory
{
    protected override ContentModel CreateBorderedContentModel(IBorderElement borderElement, Pen? borderPen, Size constraints,
        LayoutScope scope)
    {
        if (borderElement is not ITextElement textElement)
            throw new ArgumentException("Element must implement ITextElement", nameof(borderElement));
        
        Font font = textElement.GetFont(scope.Font);
        double lineHeight = textElement.LineHeight ?? scope.LineHeight;
        StringFormat stringFormat = textElement.StringFormat ?? scope.StringFormat;
        Color fontColor = textElement.FontColor ?? scope.FontColor;

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
            .Select(ll => ll.Size)
            .GetCombinedSize(Direction.Vertical);

        return new TextContentModel(lineLayouts.ToArray(),
            textElement.GetSize(contentSize),
            font,
            stringFormat,
            new SolidBrush(fontColor),
            borderPen);
    }
}