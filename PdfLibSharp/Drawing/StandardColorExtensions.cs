using PdfLibSharp.Drawing;

public static class StandardColorExtensions
{
    extension(StandardColors standardColor)
    {
        public static bool IsStandardColor(uint argb) => Enum.IsDefined(typeof(StandardColors), argb);

        public static StandardColors FromArgb(uint argb) => (StandardColors)argb;

        public uint Argb => (uint)standardColor;
        
        public Color Color => new(standardColor.Argb);
    }
}