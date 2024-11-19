namespace PdfLibSharp.Drawing.Units;

public class UnitPixel(int ppi = UnitPixel.DefaultPixelsPerInch) : Unit
{
    public const int DefaultPixelsPerInch = 96;

    public override double ToPoints(double value)
    {
        return value * ppi;
    }

    public override double FromPoints(double points)
    {
        return points / ppi;
    }

    public static UnitPixel ForPpi(int ppi = DefaultPixelsPerInch)
    {
        return new UnitPixel(ppi);
    }
}