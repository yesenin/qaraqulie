namespace yesenin.Qaraqulie.Library;

/// <summary>
/// Settings for SVG canvas
/// </summary>
public class CanvasSettings
{
    public float Width { get; set; }
    public float Height { get; set; }
    public float LeftMargin { get; set; }
    public float TopMargin { get; set; }
    public float RightMargin { get; set; }
    public float BottomMargin { get; set; }
    
    public float DrawingAreaWidth => Width - LeftMargin - RightMargin;
    public float DrawingAreaHeight => Height - TopMargin - BottomMargin;

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