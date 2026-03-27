using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Library.Abstractions;
using yesenin.Qaraqulie.Sdk;
using yesenin.Qaraqulie.Sdk.Extensions;

namespace yesenin.Qaraqulie.App.Arts;

/// <summary>
/// Demo for Hermite spline
/// </summary>
public class Art04: IArt
{
    public string GetSvg()
    {
        var g = new DrawingGroup("red", 1f);

        var ctx = CanvasSettings.DefaultLandscapeA4Context();
        
        var a = new Point(ctx.LeftMargin, ctx.TopMargin);
        var b = new Point(ctx.Width - ctx.LeftMargin - ctx.RightMargin, ctx.TopMargin);

        var lineLength = 60;

        var line1b = a with { X = a.X + lineLength };
        var line2a = b with { X = b.X - lineLength };

        var midX = (line2a.X - line1b.X) / 2;
        var midY = a.Y + 3f;
        
        var p = new Parabola(line1b, new Point(midX, midY), line2a);
        
        var line1 = new Line(a, line1b);
        var line2 = new Line(line2a, b);

        var leftFunc = (double t) => new Point(t, ctx.TopMargin);
        var rightFunc = (double t) => new Point(t, p.GetTangent(t));
        
        var leftCurve = new HermiteSpline(leftFunc, rightFunc);

        var lineDeltaX = line1b.X - 10;
        var parabolaDeltaX = line1b.X + 10;
        
        var leftSplineA = line1b with { X = lineDeltaX};
        var leftSplineB = new Point(X: parabolaDeltaX, Y: p.GetY(parabolaDeltaX));
        
        var leftSpline = leftCurve.GetPolyline(leftSplineA, leftSplineB);
        var leftA = leftCurve.GetLeftTangent(lineDeltaX, lineDeltaX + 10);
        var leftB = leftCurve.GetRightTangent(parabolaDeltaX, parabolaDeltaX - 10);
        
        var rightCurve = new HermiteSpline(leftFunc, rightFunc);
        
        var rightSplineA = line2a with { X = line2a.X + 10 };
        var rightSplineB = new Point(X: line2a.X - 10, Y: p.GetY(line2a.X - 10));
        
        var rightSpline = rightCurve.GetPolyline(rightSplineA, rightSplineB);
        
        g.WithItem(line1)
            .WithItem(leftSpline)
            .WithItem(leftA)
            .WithItem(leftB)
            .WithItem(p.GetPolyline(line1b.X, line2a.X))
            .WithItem(rightSpline)
            .WithItem(line2);
        
        var canvas = new Canvas(ctx.Width, ctx.Height).WithGroup(g);
        
        return canvas.GetSvg();
    }
}