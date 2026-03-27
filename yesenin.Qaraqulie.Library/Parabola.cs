using yesenin.Qaraqulie.Library.Abstractions;

namespace yesenin.Qaraqulie.Library;

public class Parabola : IDrawableItem
{
    private readonly double _a;
    private readonly double _b;
    private readonly double _c;
    
    public Parabola(double a, double b, double c)
    {
        _a = a;
        _b = b;
        _c = c;
    }

    public Parabola(Point left, Point middle, Point right)
    {
        /*
         * self.a = ((y_r - y_m) / (x_r - x_m) - ((y_m - y_l) / (x_m - x_l))) / (x_r - x_l)
           self.b = ((y_m - y_l) / (x_m - x_l)) - (self.a * (x_l + x_m))
           self.c = y_m - (self.a * x_m * x_m) - (self.b * x_m)
         */
        _a = ((right.Y - middle.Y) / (right.X - middle.X) - (middle.Y - left.Y) / (middle.X - left.X)) / (right.X - left.X);
        _b = (middle.Y - left.Y) / (middle.X - left.X) - _a * (left.X + middle.X);
        _c = middle.Y - _a * middle.X * middle.X - _b * middle.X;
    }
    
    public double GetY(double x) => _a * x * x + _b * x + _c;
    
    public double GetTangent(double x) => 2 * _a * x + _b;

    public string GetSvg()
    {
        return string.Empty;
    }

    public Polyline GetPolyline(double fromX, double toX)
    {
        var polyline = new Polyline(0.1f, "black");
        
        var parts = 100f;
        var step = (toX - fromX) / parts;

        var currentX = fromX;
        while (currentX <= toX)
        {
            polyline.AddPoint(new Point(currentX, GetY(currentX)));
            currentX += step;
        }
        
        return polyline;
    }
}