namespace yesenin.Qaraqulie.Library;

public abstract class Graph
{
    private const float Parts = 100f;
    
    protected string Color { get; init; } = "black";
    protected Func<double, double> F { get; set; }
    
    public double GetY(double x) => F(x);
    
    public Polyline GetPolyline(double from, double to)
    {
        var step = (to - from) / Parts;

        var currentX = from;

        var polyline = new Polyline(0.1f, Color);
        
        while (currentX <= to)
        {
            var currentY = F(currentX);
            polyline.AddPoint(currentX, currentY);
            currentX += step;
        }
        
        return polyline;
    }
}