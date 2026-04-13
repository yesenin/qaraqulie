namespace yesenin.Qaraqulie.Library;

public class CirclePath(double cx, double cy, double r) : IDrawableItem
{
    public string GetSvg()
    {
        return $"<path d=\"M {cx} {cy}" +
               $" m -{r},0" +
               $" a {r},{r} 0 1,1 {r*2}, 1" +
               $" a {r},{r} 0 1,1 -{r*2}, 0\" " +
               $"fill=\"none\" stroke=\"black\" stroke-width=\"0.2\"/>";

    }
}