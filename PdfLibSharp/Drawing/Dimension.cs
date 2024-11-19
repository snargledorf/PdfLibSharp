using PdfLibSharp.Drawing.Units;

namespace PdfLibSharp.Drawing;

public readonly struct Dimension(double value, Unit unit) : IComparable<Dimension>, IEquatable<Dimension>
{
    private readonly Unit? _unit = unit;

    public Unit Unit => _unit ?? Unit.Point;
    
    public double Value { get; } = value;

    public double Points => Unit.ToPoints(Value);

    public static Dimension operator +(Dimension left, Dimension right) =>
        new(left.Points + right.Points, Unit.Point);

    public static Dimension operator +(Dimension dimension, double points) =>
        new(dimension.Points + points, Unit.Point);

    public static implicit operator double(Dimension dimension) => dimension.Points;

    public static implicit operator Dimension(double coordinatePoints) => new(coordinatePoints, Unit.Point);

    public static Dimension FromInches(double inches) => new(inches, Unit.Inch);

    public static Dimension FromMillimeters(double millimeters) => new(millimeters, Unit.Millimeter);
    
    public static Dimension FromPixels(int pixels, int ppi = UnitPixel.DefaultPixelsPerInch) => new(pixels, UnitPixel.ForPpi(ppi));

    public void Deconstruct(out double value, out Unit unit)
    {
        value = Value;
        unit = Unit;
    }

    public void Deconstruct(out double points)
    {
        points = Points;
    }

    public int CompareTo(Dimension other)
    {
        return Points.CompareTo(other.Points);
    }

    public override bool Equals(object? other)
    {
        return other is Dimension dimension && Equals(dimension);
    }

    public bool Equals(Dimension other)
    {
        return Equals(Points, other.Points);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Points);
    }

    public static bool operator ==(Dimension left, Dimension right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Dimension left, Dimension right)
    {
        return !(left == right);
    }
}