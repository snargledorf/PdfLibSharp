using System.Text;
using PdfLib.Drawing;
using PdfLib.Elements.Content;
using PdfLib.Elements.Layout;

namespace PdfLib.Layout;

internal static class TextElementExtensions
{
    internal static ILayoutBuilder GetLayoutBuilder(this ITextElement textElement, Font font,
        StringFormat stringFormat, IMeasureGraphics measureGraphics)
    {
        font = textElement.GetFont(font);
        stringFormat = textElement.StringFormat ?? stringFormat;
        
        IReadOnlyCollection<Word> words = GetWords(textElement.Text, font, measureGraphics);
        
        Size contentSize = words.Select(word => word.Size).GetCombinedSize(Direction.Horizontal);
        contentSize = textElement.GetSize(contentSize);
        
        return new TextLayoutBuilder(textElement, words, contentSize, font, stringFormat);
    }

    private static IReadOnlyCollection<Line> GetLines(string text, Font font, IMeasureGraphics measureGraphics)
    {
        // TODO: Handle lines in text
        
        var lines = new List<Line>();
        var lineBuilder = new StringBuilder();
        Size lineSize = ContentSize with { Width = 0 };
        Point linePoint = bounds.Point;
        foreach (Word word in words)
        {
            if (lineSize.Width + word.Size.Width > bounds.Size.Width)
            {
                lines.Add(new Line(lineBuilder.ToString(), new Rectangle(linePoint, lineSize)));
                lineBuilder.Clear();
                lineSize = ContentSize with { Width = 0 };
                linePoint = linePoint with { X = linePoint.X + lineSize.Width };
            }
                
            lineBuilder.Append(word.Text);
            lineSize = lineSize with { Width = lineSize.Width + word.Size.Width };
        }
        
        lines.Add(new Line(lineBuilder.ToString(), new Rectangle(bounds.Point, lineSize));
    }

    private static IReadOnlyCollection<Word> GetWords(string text, Font font, IMeasureGraphics measureGraphics)
    {
        string[] words = text.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries);
        return words.Select(word => new Word(word, measureGraphics.MeasureString(word, font))).ToArray();
    }
}