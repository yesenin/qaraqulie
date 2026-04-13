namespace yesenin.Qaraqulie.Library.Abstractions;

/// <summary>
/// Point
/// </summary>
/// <param name="X"></param>
/// <param name="Y"></param>
public record Point (double X, double Y)
{
    public string GetSvg()
    {
        return $"{X:0.00},{Y:0.00}";
    }
    
    public Point Move(double dx, double dy)
    {
        return new Point(X + dx, Y + dy);
    }
    
    public static Point operator *(Point left, double d) => new (left.X * d, left.Y * d);
    
    public static Point operator -(Point left, Point right) => new (left.X - right.X, left.Y - right.Y);
    
    public static Point operator +(Point left, Point right) => new (left.X + right.X, left.Y + right.Y);
    
    public static Point operator /(Point left, double d) => new (left.X / d, left.Y / d);
    
    public Point MoveTo(Point p, double l)
    {
        var v = new Point(X - p.X, Y - p.Y);
        var d = Math.Sqrt(v.X * v.X + v.Y * v.Y);
        var u = v / d;
        var result = new Point(X - l * u.X, Y - l * u.Y);
        return result;
    }
    
    public Point MoveTo2(Point p, double l)
    {
        var v = new Point(X - p.X, Y - p.Y);
        var d = Math.Sqrt(v.X * v.X + v.Y * v.Y);
        var u = v / d;
        var result = new Point(X + l * u.X, Y + l * u.Y);
        return result;
    }
}