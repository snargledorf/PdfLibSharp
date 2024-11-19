namespace PdfLibSharp.Drawing.Units;

public class UnitMillimeter : Unit
{
    public const double PointsPerMillimeter = 2.8346456693;

    public override double PointsPerUnit => PointsPerMillimeter;
}