using yesenin.Qaraqulie.Library.Abstractions;

namespace yesenin.Qaraqulie.Library;

public class Polyline(double strokeWidth, string color) : IDrawableItem
{
    private readonly List<Point> _points = [];
    
    public void AddPoint(Point point)
    {
        _points.Add(point);
    }
    
    public void AddPoint(double x, double y) => AddPoint(new Point(x, y));
    
    public void Reverse() => _points.Reverse();

    public string GetSvg()
    {
        var pointsString = string.Join(" ", _points.Select(p => p.GetSvg()));
        return $"<polyline points=\"{pointsString}\" fill=\"none\" stroke=\"{color}\" stroke-width=\"{strokeWidth}\"/>";
    }
}