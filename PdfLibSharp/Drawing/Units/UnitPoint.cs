namespace PdfLibSharp.Drawing.Units;

public sealed class UnitPoint : Unit
{
    public override double PointsPerUnit => 1;

    public override double ToPoints(double value)
    {
        return value;
    }

    public override double FromPoints(double points)
    {
        return points;
    }
}