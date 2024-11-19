namespace PdfLibSharp.Drawing.Units;

public class UnitPixel(int ppi = UnitPixel.DefaultPixelsPerInch) : Unit
{
    public const int DefaultPixelsPerInch = 96;
    
    private readonly double _conversionFactor = PointsPerInch / ppi;

    public override double ToPoints(double value)
    {
        return value * _conversionFactor;
    }

    public override double FromPoints(double points)
    {
        return points / _conversionFactor;
    } 

    public static UnitPixel ForPpi(int ppi = DefaultPixelsPerInch)
    {
        return new UnitPixel(ppi);
    }
}