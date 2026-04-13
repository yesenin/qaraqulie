using System.Security.Cryptography;
using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Library.Abstractions;
using yesenin.Qaraqulie.Sdk;
using yesenin.Qaraqulie.Sdk.Extensions;
using Path = yesenin.Qaraqulie.Library.Path;

namespace yesenin.Qaraqulie.App.Arts;

/// <summary>
/// A sample of spline between ends of lines with control points
/// </summary>
public class Art05 : IArt
{
    public string GetSvg()
    {
        
        var ctx = CanvasSettings.DefaultLandscapeA4Context();
        var canvas = new Canvas(ctx.Width, ctx.Height);
        for (var i = 0; i < 3; i++)
        {
            var k1 = 2 - 0.1 * i;
            var k2 = 0.1 + 0.1 * i;
            var lineA = new GraphLine(k1, 0, "red");
            var lineB = new GraphLine(k2, 50, "blue");

            var point0 = new Dot(40, lineA.GetY(40));
            var point1 = new Dot(60, lineB.GetY(60));

            var g = new DrawingGroup("red", 1f);

            g.AddItem(lineA.GetPolyline(ctx.LeftMargin, ctx.Width - ctx.RightMargin));
            g.AddItem(lineB.GetPolyline(ctx.LeftMargin, ctx.Width - ctx.RightMargin));
            g.AddItem(point0);
            g.AddItem(point1);
            g.AddItem(GetPath(40, 60, lineA.GetY, lineB.GetY, k1, k2));

            canvas.WithGroup(g);
        }

        return canvas.GetSvg();
    }
    
    private Point Normalize(Point v)
    {
        var len = Math.Sqrt(v.X * v.X + v.Y * v.Y);
        return new Point(v.X / len, v.Y / len);
    }

    private Path GetPath(double a, double b, Func<double, double> f1,  Func<double, double> f2, double k1, double k2)
    {
        var start = new Point(a + 10, f1(a + 10));

        var p0 = new Point(a, f1(a)); //new Point(40, 80);
        var p1 = new Point(b, f2(b)); //new Point(60, 80);

        var r0 = Normalize(new Point(1, k1));
        var r1 = Normalize(new Point(1, k2));

        double h = 15;

        var c1 = p0 - r0 * h;
        var c2 = p1 - r1 * h;

        var end = new Point(b + 10, f2(b + 10)); // дальше по синей

        var path = new Path();
        path.AddLineTo(start);
        path.AddLineTo(p0);
        path.AddCurve(c1, c2, p1);
        path.AddLineTo(end);

        return path;
    }
}