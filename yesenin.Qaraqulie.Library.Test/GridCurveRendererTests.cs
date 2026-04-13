using FluentAssertions;
using yesenin.Qaraqulie.Library.Abstractions;
using yesenin.Qaraqulie.Sdk.Grid;
using yesenin.Qaraqulie.Sdk.Grid.Renderers;

namespace yesenin.Qaraqulie.Library.Test;

public class GridCurveRendererTests
{
    [Fact]
    public void HorizontalRendererShouldBuildOneCurvePerSegment()
    {
        var grid = CreateHorizontalGrid(
            new Point(0, 0),
            new Point(10, 0),
            new Point(20, 10),
            new Point(30, 10));
        var settings = new GridSettings
        {
            Width = 4,
            Height = 2,
            Parts = 1,
            ShakeIntensity = 0,
            Name = "test"
        };

        var svg = new GridCurveRenderer().Render(grid, settings).GetSvg();
        var pathData = ExtractPathData(svg);

        pathData.Should().Contain("C");
        pathData.Should().NotContain("L ");
        pathData.Should().StartWith("M 0.00 0.00");
        pathData.Should().EndWith("30.00 10.00");
        svg.Should().Contain("M 0.00 0.00");
        svg.Should().Contain("30.00 10.00");
        pathData.Split('C').Length.Should().Be(4);
    }

    [Fact]
    public void VerticalRendererShouldBuildOneCurvePerSegment()
    {
        var grid = CreateVerticalGrid(
            new Point(0, 0),
            new Point(0, 10),
            new Point(10, 20),
            new Point(10, 30));
        var settings = new GridSettings
        {
            Width = 2,
            Height = 4,
            Parts = 1,
            ShakeIntensity = 0,
            Name = "test"
        };

        var svg = new GridVerticalCurveRenderer().Render(grid, settings).GetSvg();
        var pathData = ExtractPathData(svg);

        pathData.Should().Contain("C");
        pathData.Should().NotContain("L ");
        pathData.Should().StartWith("M 0.00 0.00");
        pathData.Should().EndWith("10.00 30.00");
        svg.Should().Contain("M 0.00 0.00");
        svg.Should().Contain("10.00 30.00");
        pathData.Split('C').Length.Should().Be(4);
    }

    private static Grid CreateHorizontalGrid(params Point[] points)
    {
        var grid = new Grid(DefaultContext(), new GridSettings
        {
            Width = points.Length,
            Height = 2,
            Parts = 1,
            ShakeIntensity = 0,
            Name = "test"
        });

        grid.Points.Clear();
        grid.Points.Add(points);
        return grid;
    }

    private static Grid CreateVerticalGrid(params Point[] points)
    {
        var grid = new Grid(DefaultContext(), new GridSettings
        {
            Width = 2,
            Height = points.Length,
            Parts = 1,
            ShakeIntensity = 0,
            Name = "test"
        });

        grid.Points.Clear();
        foreach (var point in points)
        {
            grid.Points.Add(new[] { point, point.Move(10, 0) });
        }

        return grid;
    }

    private static CanvasSettings DefaultContext()
    {
        return new CanvasSettings
        {
            Width = 100,
            Height = 100,
            TopMargin = 0,
            BottomMargin = 0,
            LeftMargin = 0,
            RightMargin = 0
        };
    }

    private static string ExtractPathData(string svg)
    {
        var marker = "d=\"";
        var start = svg.IndexOf(marker, StringComparison.Ordinal);
        start.Should().BeGreaterThanOrEqualTo(0);
        start += marker.Length;

        var end = svg.IndexOf("\"", start, StringComparison.Ordinal);
        end.Should().BeGreaterThan(start);

        return svg[start..end];
    }
}
