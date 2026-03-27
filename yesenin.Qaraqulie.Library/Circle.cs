namespace yesenin.Qaraqulie.Library;

/// <summary>
/// Circle, but used as a point
/// </summary>
/// <param name="x"></param>
/// <param name="y"></param>
/// <param name="color"></param>
public class Circle(double x, double y, string color = "pink") : IDrawableItem
{
    public string GetSvg()
    {
        return $"<circle cx=\"{x}\" cy=\"{y}\" r=\"0.2mm\" fill=\"{color}\" stroke=\"black\" stroke-width=\"0.2\"/>";
    }
}