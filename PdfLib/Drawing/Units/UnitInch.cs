namespace PdfLib.Drawing.Units;

internal sealed class UnitInch : Unit
{
    public override double ToPoints(double value)
    {
        return value * PointsPerInch;
    }

    public override double FromPoints(double points)
    {
        return points / PointsPerInch;
    }
}