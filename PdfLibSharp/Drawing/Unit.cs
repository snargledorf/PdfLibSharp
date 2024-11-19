using PdfLibSharp.Drawing.Units;

namespace PdfLibSharp.Drawing;

public abstract class Unit
{
    public const double PointsPerInch = 72;
    public const double PointsPerMillimeter = 2.8346456693;

    public static readonly UnitInch Inch = new();
    public static readonly UnitMillimeter Millimeter = new();
    public static readonly UnitPoint Point = new();
    public static readonly UnitPixel Pixel = new();
    
    public abstract double ToPoints(double value);
    public abstract double FromPoints(double points);
}