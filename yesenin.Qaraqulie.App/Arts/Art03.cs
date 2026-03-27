using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Sdk;
using yesenin.Qaraqulie.Sdk.Extensions;

namespace yesenin.Qaraqulie.App.Arts;

/// <summary>
/// Two simple sine waves
/// </summary>
public class Art03 : IArt
{
    public string GetSvg()
    {
        var x = (float t) => t * t * 5f;
        var y = (float t) => (float)Math.Sin(2*t)*10 + 20;
        var a = new ParametricCurve(x, y);
        
        var g = new DrawingGroup("black", 0.1f);

        var tMin = -10f;
        var tMax = 10f;
        var precision = 100f;
        
        var step = (tMax - tMin) / precision;

        var t = tMin;
        
        var polyline = new Polyline(0.1f, "black");
        while (t <= tMax)
        {
            polyline.AddPoint(a.GetPoint(t));
            t += step;
        }
        
        g.AddItem(polyline);
        
        return new Canvas(297f, 210f).WithGroup(g).GetSvg();
    }
}