using PdfLibrary.Drawing.Units;

namespace PdfLibrary.Drawing;

public abstract class Unit
{
    public const double PointsPerInch = 72;
    public const double PointsPerMillimeter = 2.8346456693;
    
    public static readonly Unit Inch = new UnitInch();
    public static readonly Unit Millimeter = new UnitMillimeter();
    public static readonly Unit Point = new UnitPoint();
    
    public abstract double ToPoints(double value);
    public abstract double FromPoints(double points);
}