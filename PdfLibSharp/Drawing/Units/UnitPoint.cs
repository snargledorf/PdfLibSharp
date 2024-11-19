namespace PdfLibSharp.Drawing.Units;

public sealed class UnitPoint : Unit
{
    public override double ToPoints(double value)
    {
        return value;
    }

    public override double FromPoints(double points)
    {
        return points;
    }
}