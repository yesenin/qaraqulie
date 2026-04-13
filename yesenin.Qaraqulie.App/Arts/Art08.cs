using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Library.Abstractions;
using yesenin.Qaraqulie.Sdk.Extensions;

namespace yesenin.Qaraqulie.App.Arts;

public class Art08 : IArt
{
    private readonly CanvasSettings _ctx;
    
    public Art08()
    {
        _ctx = CanvasSettings.DefaultLandscapeA4Context();
    }
    
    public string GetSvg()
    {
        var g = new DrawingGroup("red", 0.1f);

        var parts = 1;
        var gutter = 5;

        var blockHeight = 30; //_ctx.Height / parts;
        
        var p1 = new Point(_ctx.LeftMargin, _ctx.TopMargin);
        var p2 = new Point(_ctx.Width - _ctx.RightMargin, _ctx.TopMargin);
        var p3 = new Point(_ctx.Width - _ctx.RightMargin, _ctx.TopMargin + blockHeight);
        var p4 = new Point(_ctx.LeftMargin, _ctx.TopMargin + blockHeight);
        
        g.AddItem(new Line(p1, p2));
        g.AddItem(new Line(p2, p3));
        g.AddItem(new Line(p3, p4));
        g.AddItem(new Line(p4, p1));
        
        AddLines(g, _ctx.TopMargin, _ctx.TopMargin + 30, 0.005);
        AddLines(g, _ctx.TopMargin + 35, _ctx.TopMargin + 65, 0.003);
        
        var svg = new Canvas(_ctx)
            .WithGroup(g)
            .GetSvg();
        return svg;
    }
    
    private void AddLines(DrawingGroup g, double top, double bottom, double a)
    {
        var x = _ctx.LeftMargin + 2;
        var p = 1;
        var k = -50;
        while (x < _ctx.DrawingAreaWidth)
        {
            var q1 = new Point(x, top);
            var q2 = new Point(x, bottom);
            g.AddItem(new Line(q1, q2));
            x += a * k * k;
            p++;
            k += 1;
        }
    }
}