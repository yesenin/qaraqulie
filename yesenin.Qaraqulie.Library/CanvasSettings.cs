using yesenin.Qaraqulie.Library.Abstractions;

namespace yesenin.Qaraqulie.Library;

/// <summary>
/// Settings for SVG canvas
/// </summary>
public class CanvasSettings
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double LeftMargin { get; set; }
    public double TopMargin { get; set; }
    public double RightMargin { get; set; }
    public double BottomMargin { get; set; }
    
    public Point TopLeft => new Point(LeftMargin, TopMargin);
    public Point BottomRight => new Point(Width - RightMargin, Height - BottomMargin);
    public Point Center => new Point(Width / 2, Height / 2);
    public Point TopRight => new Point(Width - RightMargin, TopMargin);
    public Point BottomLeft => new Point(LeftMargin, Height - BottomMargin);
    
    public double DrawingAreaWidth => Width - LeftMargin - RightMargin;
    public double DrawingAreaHeight => Height - TopMargin - BottomMargin;
    
    public static CanvasSettings DefaultLandscapeA4Context()
    {
        return new CanvasSettings
        {
            LeftMargin = 10,
            TopMargin = 10,
            RightMargin = 10,
            BottomMargin = 10,
            Width = 297f,
            Height = 210f
        };
    }
    
    public static CanvasSettings DefaultPortraitA4Context()
    {
        return new CanvasSettings
        {
            LeftMargin = 10,
            TopMargin = 10,
            RightMargin = 10,
            BottomMargin = 10,
            Width = 210f,
            Height = 297f
        };
    }
}