namespace PdfLibSharp.Drawing.Units;

public class UnitPixel(int ppi = UnitPixel.DefaultPixelsPerInch) : Unit
{
    public const int DefaultPixelsPerInch = 96;

    public override double PointsPerUnit { get; } = UnitInch.PointsPerInch / ppi;

    public static UnitPixel ForPpi(int ppi)
    {
        return new UnitPixel(ppi);
    }
}