namespace yesenin.Qaraqulie.Library;

public class Circle(double cx, double cy, double r) : IDrawableItem
{
    public string GetSvg()
    {
        return $"<circle cx=\"{cx}\" cy=\"{cy}\" r=\"{r}\" fill=\"none\" stroke=\"black\" stroke-width=\"0.2\"/>";

    }
}