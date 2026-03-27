using System.Drawing;
using System.Text;

namespace yesenin.Qaraqulie.Library;

/// <summary>
/// A wrapper for SVG canvas
/// </summary>
public class Canvas(double width, double height) : IDrawable
{
    public Canvas(CanvasSettings ctx) : this(ctx.Width, ctx.Height)
    {
    }
    
    private List<DrawingGroup> _groups = new List<DrawingGroup>();
    public double Width { get; set; } = width;
    public double Height { get; set; } = height;

    public void AddGroup(DrawingGroup group)
    {
        _groups.Add(group);
    }

    public string GetSvg()
    {
        var svgString = new StringBuilder();
        svgString.AppendLine("<svg xmlns=\"http://www.w3.org/2000/svg\"");
        svgString.AppendLine($"width=\"{Width}mm\" height=\"{Height}mm\"");
        svgString.AppendLine($"viewBox=\"0 0 {Width} {Height}\"");
        svgString.AppendLine(">");
        foreach (var group in _groups)
        {
            svgString.Append(group.GetSvg());
        }
        svgString.AppendLine("</svg>");
        return svgString.ToString();
    }
}