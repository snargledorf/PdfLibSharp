namespace PdfLibrary.Drawing;

public readonly record struct Dimension(double Value, Unit? Unit) : IComparable<Dimension>
{
    private readonly Unit _unit = Unit ?? Unit.Point;

    public Unit Unit => _unit ?? Unit.Point;

    public double Points => Unit.ToPoints(Value);

    public static Dimension operator +(Dimension left, Dimension right) =>
        new(left.Points + right.Points, Unit.Point);

    public static Dimension operator +(Dimension dimension, double points) =>
        new(dimension.Points + points, Unit.Point);

    public static implicit operator double(Dimension dimension) => dimension.Points;

    public static implicit operator Dimension(double coordinatePoints) => new(coordinatePoints, Unit.Point);

    public static Dimension FromInches(double inches) => new(inches, Unit.Inch);

    public static Dimension FromMillimeters(double millimeters) => new(millimeters, Unit.Millimeter);

    public void Deconstruct(out double Value, out Unit Unit)
    {
        Value = this.Value;
        Unit = this.Unit;
    }

    public int CompareTo(Dimension other)
    {
        return Value.CompareTo(other.Value);
    }
}