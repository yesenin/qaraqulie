"""Tests for generate.py"""

import math
import os
import sys
import tempfile

import pytest

# Ensure the project root is on the path when tests are run directly.
sys.path.insert(0, os.path.join(os.path.dirname(__file__), ".."))

from generate import (
    SvgCanvas,
    concentric_circles,
    grid_lines,
    lissajous,
    main,
    spirograph,
    wave_pattern,
)


# ---------------------------------------------------------------------------
# SvgCanvas
# ---------------------------------------------------------------------------


class TestSvgCanvas:
    def test_empty_canvas_produces_valid_svg(self):
        canvas = SvgCanvas(210, 297)
        svg = canvas.to_svg()
        assert svg.startswith("<?xml")
        assert "<svg" in svg
        assert "</svg>" in svg

    def test_dimensions_appear_in_svg(self):
        canvas = SvgCanvas(100, 200, unit="px")
        svg = canvas.to_svg()
        assert 'width="100px"' in svg
        assert 'height="200px"' in svg
        assert 'viewBox="0 0 100 200"' in svg

    def test_add_path_appears_in_svg(self):
        canvas = SvgCanvas(100, 100)
        canvas.add_path([(0, 0), (50, 50), (100, 0)])
        svg = canvas.to_svg()
        assert "<polyline" in svg
        assert 'fill="none"' in svg

    def test_add_path_with_fewer_than_two_points_is_ignored(self):
        canvas = SvgCanvas(100, 100)
        canvas.add_path([])
        canvas.add_path([(10, 10)])
        svg = canvas.to_svg()
        assert "<polyline" not in svg

    def test_add_circle_appears_in_svg(self):
        canvas = SvgCanvas(100, 100)
        canvas.add_circle(50, 50, 30)
        svg = canvas.to_svg()
        assert "<circle" in svg
        assert 'fill="none"' in svg

    def test_save_writes_file(self):
        canvas = SvgCanvas(100, 100)
        canvas.add_circle(50, 50, 20)
        with tempfile.NamedTemporaryFile(suffix=".svg", delete=False) as tmp:
            path = tmp.name
        try:
            canvas.save(path)
            assert os.path.exists(path)
            with open(path, encoding="utf-8") as fh:
                content = fh.read()
            assert "<svg" in content
        finally:
            os.unlink(path)


# ---------------------------------------------------------------------------
# spirograph
# ---------------------------------------------------------------------------


class TestSpirograph:
    def test_returns_correct_number_of_points(self):
        pts = spirograph(40, 8, 10, 50, 50, steps=100)
        assert len(pts) == 101  # steps + 1

    def test_curve_is_closed(self):
        pts = spirograph(40, 8, 10, 0, 0, steps=1000)
        # First and last points should be the same (closed curve)
        assert math.isclose(pts[0][0], pts[-1][0], abs_tol=1e-6)
        assert math.isclose(pts[0][1], pts[-1][1], abs_tol=1e-6)

    def test_center_offset_applied(self):
        pts_origin = spirograph(40, 8, 10, 0, 0, steps=100)
        pts_offset = spirograph(40, 8, 10, 50, 70, steps=100)
        for (x0, y0), (x1, y1) in zip(pts_origin, pts_offset):
            assert math.isclose(x1 - x0, 50, abs_tol=1e-6)
            assert math.isclose(y1 - y0, 70, abs_tol=1e-6)


# ---------------------------------------------------------------------------
# lissajous
# ---------------------------------------------------------------------------


class TestLissajous:
    def test_returns_correct_number_of_points(self):
        pts = lissajous(3, 2, 0, 100, 80, 50, 50, steps=500)
        assert len(pts) == 501

    def test_points_within_bounding_box(self):
        cx, cy, w, h = 50, 50, 80, 60
        pts = lissajous(3, 2, math.pi / 4, w, h, cx, cy, steps=1000)
        for x, y in pts:
            assert cx - w / 2 - 1e-9 <= x <= cx + w / 2 + 1e-9
            assert cy - h / 2 - 1e-9 <= y <= cy + h / 2 + 1e-9


# ---------------------------------------------------------------------------
# concentric_circles
# ---------------------------------------------------------------------------


class TestConcentricCircles:
    def test_returns_correct_count(self):
        circles = concentric_circles(50, 50, 5, 45, 10)
        assert len(circles) == 10

    def test_first_and_last_radii(self):
        circles = concentric_circles(50, 50, 5, 45, 10)
        assert math.isclose(circles[0][2], 5)
        assert math.isclose(circles[-1][2], 45)

    def test_single_circle(self):
        circles = concentric_circles(10, 20, 7, 30, 1)
        assert len(circles) == 1
        assert circles[0] == (10, 20, 7)

    def test_center_unchanged(self):
        for cx, cy, r in concentric_circles(30, 40, 1, 10, 5):
            assert cx == 30
            assert cy == 40


# ---------------------------------------------------------------------------
# grid_lines
# ---------------------------------------------------------------------------


class TestGridLines:
    def test_returns_correct_number_of_paths(self):
        paths = grid_lines(0, 0, 100, 100, 4, 3)
        # (cols+1) verticals + (rows+1) horizontals
        assert len(paths) == 5 + 4

    def test_each_path_has_two_endpoints(self):
        for path in grid_lines(0, 0, 100, 100, 5, 5):
            assert len(path) == 2


# ---------------------------------------------------------------------------
# wave_pattern
# ---------------------------------------------------------------------------


class TestWavePattern:
    def test_returns_correct_number_of_rows(self):
        paths = wave_pattern(0, 0, 100, 100, 5, 3, 4, steps_per_row=50)
        assert len(paths) == 5

    def test_each_row_has_correct_number_of_points(self):
        for path in wave_pattern(0, 0, 100, 100, 3, 2, 3, steps_per_row=100):
            assert len(path) == 101  # steps_per_row + 1

    def test_single_row(self):
        paths = wave_pattern(0, 10, 100, 10, 1, 2, 3, steps_per_row=50)
        assert len(paths) == 1


# ---------------------------------------------------------------------------
# CLI (main)
# ---------------------------------------------------------------------------


class TestCLI:
    @pytest.mark.parametrize(
        "pattern",
        ["spirograph", "lissajous", "circles", "grid", "waves"],
    )
    def test_all_patterns_produce_svg_file(self, pattern, tmp_path):
        output = str(tmp_path / f"{pattern}.svg")
        rc = main([pattern, "-o", output])
        assert rc == 0
        assert os.path.exists(output)
        with open(output, encoding="utf-8") as fh:
            content = fh.read()
        assert "<svg" in content
        assert "</svg>" in content

    def test_custom_dimensions(self, tmp_path):
        output = str(tmp_path / "custom.svg")
        rc = main(["grid", "-o", output, "--width", "150", "--height", "200"])
        assert rc == 0
        with open(output, encoding="utf-8") as fh:
            content = fh.read()
        assert 'width="150.0mm"' in content
        assert 'height="200.0mm"' in content

    def test_default_output_filename(self, tmp_path, monkeypatch):
        monkeypatch.chdir(tmp_path)
        rc = main(["circles"])
        assert rc == 0
        assert os.path.exists(tmp_path / "circles.svg")

    def test_invalid_pattern_exits(self):
        with pytest.raises(SystemExit) as exc_info:
            main(["unknown_pattern"])
        assert exc_info.value.code != 0
