using yesenin.Qaraqulie.Library.Abstractions;

namespace yesenin.Qaraqulie.Library;

/// <summary>
/// Line
/// </summary>
/// <param name="A">Start point</param>
/// <param name="B">End point</param>
public class Line(Point A, Point B): IDrawableItem
{
    public string GetSvg()
    {
        var pointsString = string.Join(" ", A.GetSvg(), B.GetSvg());
        return $"<polyline points=\"{pointsString}\" fill=\"none\"/>";
    }
}