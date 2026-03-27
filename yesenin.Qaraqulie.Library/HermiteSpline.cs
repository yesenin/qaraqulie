using yesenin.Qaraqulie.Library.Abstractions;

namespace yesenin.Qaraqulie.Library;

public class HermiteSpline
{
    private readonly Func<double, Point> _leftFunction;
    private readonly Func<double, Point> _rightFunction;
    
    public HermiteSpline(Func<double, Point> leftFunction, Func<double, Point> rightFunction)
    {
        _leftFunction = leftFunction;
        _rightFunction = rightFunction;
    }

    public Polyline GetLeftTangent(double fromT, double toT)
    {
        var polyline = new Polyline(1f, "blue");
        
        polyline.AddPoint(_leftFunction(fromT));
        polyline.AddPoint(_leftFunction(toT));
        
        return polyline;
    }
    
    public Polyline GetRightTangent(double fromT, double toT)
    {
        var polyline = new Polyline(1f, "yellow");
        
        polyline.AddPoint(_rightFunction(fromT));
        polyline.AddPoint(_rightFunction(toT));
        
        return polyline;
    }
    
    public Polyline GetPolyline(Point from, Point to)
    {
        var polyline = new Polyline(1f, "green");

        polyline.AddPoint(from);
        polyline.AddPoint(to);
        
        return polyline;
    }
}