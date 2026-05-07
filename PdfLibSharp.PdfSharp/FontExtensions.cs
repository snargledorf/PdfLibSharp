using System;
using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfSharp.Drawing;

namespace PdfLibSharp.PdfSharp;

public static class FontExtensions
{
    extension(Font font)
    {
        public XFont ToXFont() => new(font.Family, font.Size, font.FontStyles.ToXFontStyleEx());
        public static Font FromXFont(XFont xFont) => new(xFont.FontFamily.Name, xFont.Size, xFont.Style.ToFontStyles());
    }

    extension(XFont xFont)
    {
        public Font ToFont() => new(xFont.FontFamily.Name, xFont.Size, xFont.Style.ToFontStyles());
    }
    
    extension(XFontStyleEx xFontStyleEx)
    {
        public FontStyles ToFontStyles() => xFontStyleEx switch
        {
            XFontStyleEx.Regular => FontStyles.Normal,
            XFontStyleEx.Bold => FontStyles.Bold,
            XFontStyleEx.Italic => FontStyles.Italic,
            XFontStyleEx.BoldItalic => FontStyles.BoldItalic,
            XFontStyleEx.Underline => FontStyles.Underline,
            XFontStyleEx.Strikeout => FontStyles.Strikeout,
            _ => throw new ArgumentOutOfRangeException(nameof(xFontStyleEx), xFontStyleEx, null)
        };
    }
    
    extension(FontStyles fontStyles)
    {
        public XFontStyleEx ToXFontStyleEx() => fontStyles switch
        {
            FontStyles.Normal => XFontStyleEx.Regular,
            FontStyles.Bold => XFontStyleEx.Bold,
            FontStyles.Italic => XFontStyleEx.Italic,
            FontStyles.BoldItalic => XFontStyleEx.BoldItalic,
            FontStyles.Underline => XFontStyleEx.Underline,
            FontStyles.Strikeout => XFontStyleEx.Strikeout,
            _ => throw new ArgumentOutOfRangeException(nameof(fontStyles), fontStyles, null)
        };
    }
}