using PdfLibSharp.Drawing.Units;

namespace PdfLibSharp.Drawing;

public abstract class Unit : IEquatable<Unit>
{
    public static readonly UnitInch Inch = new();
    public static readonly UnitMillimeter Millimeter = new();
    public static readonly UnitPoint Point = new();
    public static readonly UnitPixel Pixel = new();
    
    public abstract double PointsPerUnit { get; }

    public virtual double ToPoints(double value)
    {
        return value * PointsPerUnit;
    }

    public virtual double FromPoints(double points)
    {
        return points / PointsPerUnit;
    }

    public override bool Equals(object? obj)
    {
        return obj is Unit unit && Equals(unit);;
    }

    public bool Equals(Unit? other)
    {
        return other is not null && Equals(other.PointsPerUnit, PointsPerUnit);
    }

    public override int GetHashCode()
    {
        return nameof(Unit).GetHashCode();
    }
}