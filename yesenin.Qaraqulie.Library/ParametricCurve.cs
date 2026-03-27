using yesenin.Qaraqulie.Library.Abstractions;

namespace yesenin.Qaraqulie.Library;

public class ParametricCurve
{
    private readonly Func<float, float> _xFunction;
    private readonly Func<float, float> _yFunction;
    
    public ParametricCurve(Func<float, float> xFunction, Func<float, float> yFunction)
    {
        _xFunction = xFunction;
        _yFunction = yFunction;
    }
    
    public Point GetPoint(float t) => new Point(_xFunction(t), _yFunction(t));
}