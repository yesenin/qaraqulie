namespace yesenin.Qaraqulie.Library;

public class GraphLine : Graph
{
    public GraphLine(double k, double b, string color = "black")
    {
        Color = color;
        F = x => k*x + b;
    }
    
    public GraphLine(Func<double, double> f) => F = f;
}