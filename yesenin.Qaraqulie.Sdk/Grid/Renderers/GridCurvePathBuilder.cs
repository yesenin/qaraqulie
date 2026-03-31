using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Library.Abstractions;
using Path = yesenin.Qaraqulie.Library.Path;

namespace yesenin.Qaraqulie.Sdk.Grid.Renderers;

internal static class GridCurvePathBuilder
{
    private const double DefaultControlLength = 3.0;
    private const double Epsilon = 1e-9;

    public static void AddSmoothPath(Path path, IReadOnlyList<Point> points, double maxControlLength = DefaultControlLength)
    {
        if (points.Count == 0)
        {
            return;
        }

        path.AddLineTo(points[0]);

        if (points.Count == 1)
        {
            return;
        }

        for (var i = 0; i < points.Count - 1; i++)
        {
            var start = points[i];
            var end = points[i + 1];
            var segmentLength = Distance(start, end);

            if (segmentLength <= Epsilon)
            {
                path.AddLineTo(end);
                continue;
            }

            var startTangent = GetTangent(points, i);
            var endTangent = GetTangent(points, i + 1);
            var controlLength = Math.Min(maxControlLength, segmentLength / 3.0);

            if (controlLength <= Epsilon || IsZero(startTangent) || IsZero(endTangent))
            {
                path.AddLineTo(end);
                continue;
            }

            var control1 = new Point(
                start.X + startTangent.X * controlLength,
                start.Y + startTangent.Y * controlLength);

            var control2 = new Point(
                end.X - endTangent.X * controlLength,
                end.Y - endTangent.Y * controlLength);

            path.AddCurve(control1, control2, end);
        }
    }

    private static Point GetTangent(IReadOnlyList<Point> points, int index)
    {
        var previous = index > 0 ? points[index - 1] : points[index];
        var next = index < points.Count - 1 ? points[index + 1] : points[index];

        var vector = new Point(next.X - previous.X, next.Y - previous.Y);
        var length = Distance(vector);

        if (length <= Epsilon)
        {
            return new Point(0, 0);
        }

        return new Point(vector.X / length, vector.Y / length);
    }

    private static bool IsZero(Point point)
    {
        return Math.Abs(point.X) <= Epsilon && Math.Abs(point.Y) <= Epsilon;
    }

    private static double Distance(Point a, Point b)
    {
        return Distance(new Point(a.X - b.X, a.Y - b.Y));
    }

    private static double Distance(Point v)
    {
        return Math.Sqrt(v.X * v.X + v.Y * v.Y);
    }
}
