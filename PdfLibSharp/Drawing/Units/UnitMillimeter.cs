namespace PdfLibSharp.Drawing.Units;

public class UnitMillimeter : Unit
{
    public override double ToPoints(double value)
    {
        return value * PointsPerMillimeter;
    }

    public override double FromPoints(double points)
    {
        return points / PointsPerMillimeter;
    }
}