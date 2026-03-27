using yesenin.Qaraqulie.Library.Abstractions;

namespace yesenin.Qaraqulie.Library;

/// <summary>
/// A wrapper for SVG path
/// </summary>
public class Path : IDrawableItem
{
    private readonly List<string> _fragments = [];
    
    public void AddLineTo(Point p)
    {
        if (_fragments.Count == 0)
        {
            _fragments.Add($"M {p.X:F2} {p.Y:F2}");
        }
        else
        {
            _fragments.Add($"L {p.X:F2} {p.Y:F2}");
        }
    }

    // TODO: add all other types of path codes
    public void AddSmoothCubicBezier(Point a, Point b)
    {
        _fragments.Add($"Q {a.X:F2} {a.Y:F2} {b.X:F2} {b.Y:F2}");
    }

    public void AddCurve(Point p1, Point p2, Point p3)
    {
        _fragments.Add($"C {p1.X:F2} {p1.Y:F2} {p2.X:F2} {p2.Y:F2}  {p3.X:F2} {p3.Y:F2}");
    }
    
    public string GetSvg()
    {
        var d = string.Join(Environment.NewLine, _fragments);
        return $"<path d=\"{d}\" fill=\"none\" stroke=\"black\" stroke-width=\"0.3\"/>";
    }
}