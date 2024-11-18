using PdfLib.Drawing;
using PdfLib.Elements.Content;
using PdfLib.Elements.Layout;

namespace PdfLib.Layout;

internal static class TextElementExtensions
{
    internal static ILayoutBuilder GetLayoutBuilder(this ITextElement textElement, Font font,
        Color fontColor,
        StringFormat stringFormat, IMeasureGraphics measureGraphics)
    {
        font = textElement.GetFont(font);
        stringFormat = textElement.StringFormat ?? stringFormat;
        fontColor = textElement.FontColor ?? fontColor;
        
        IReadOnlyCollection<IReadOnlyCollection<Word>> lines = GetLines(textElement.Text, font, measureGraphics);

        Size contentSize;
        if (lines.Count == 0)
        {
            contentSize = new Size();
        }
        else
        {
            contentSize = lines
                .Select(line => line.Select(word => word.Size).GetCombinedSize(Direction.Horizontal))
                .GetCombinedSize(Direction.Vertical);
        }

        return new TextLayoutBuilder(textElement, lines, contentSize, font, fontColor, stringFormat);
    }

    private static IReadOnlyCollection<IReadOnlyCollection<Word>> GetLines(string text, Font font, IMeasureGraphics measureGraphics)
    {
        var lines = new List<IReadOnlyCollection<Word>>();
        var lineWords = new List<Word>();

        IEnumerable<Word> words = GetWords(text, font, measureGraphics);
        foreach (Word word in words)
        {
            if (word.Text == Environment.NewLine || word.Text == "\n" || word.Text == "\r")
            {
                lines.Add(lineWords.ToArray());
                lineWords.Clear();
            }
            else
            {
                lineWords.Add(word);
            }
        }
        
        if (lineWords.Count > 0)
            lines.Add(lineWords.ToArray());
        
        return lines.ToArray();
    }

    private static IEnumerable<Word> GetWords(string text, Font font, IMeasureGraphics measureGraphics)
    {
        IEnumerable<string> words = text.SplitBy(" ")
            .SplitBy("\t")
            .SplitBy("\r\n");

        foreach (string word in words)
            yield return new Word(word, measureGraphics.MeasureString(word, font));
    }

    private static IEnumerable<string> SplitBy(this string str, string splitChar)
    {
        return Enumerable.Repeat(str, 1).SplitBy(splitChar);
    }

    private static IEnumerable<string> SplitBy(this IEnumerable<string> strs, string splitChar)
    {
        foreach (string str in strs)
        {
            string[] words = str.Split(splitChar);
            bool firstWord = true;
            
            foreach (string word in words)
            {
                if (!firstWord)
                {
                    yield return splitChar;
                }
                else
                {
                    firstWord = false;
                }
                
                yield return word;
            }
        }
    }
}