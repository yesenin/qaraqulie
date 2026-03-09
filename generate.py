#!/usr/bin/env python3
"""Generate SVG sources for pen plotter art."""

import argparse
import math
import sys
from typing import List, Tuple

# Type aliases
Point = Tuple[float, float]
Path = List[Point]


class SvgCanvas:
    """Minimal SVG canvas that produces pen-plotter-friendly output.

    All shapes are rendered as strokes with no fill, which is the standard
    format expected by pen plotters.
    """

    def __init__(self, width: float, height: float, unit: str = "mm") -> None:
        self.width = width
        self.height = height
        self.unit = unit
        self._elements: List[str] = []

    # ------------------------------------------------------------------
    # Shape primitives
    # ------------------------------------------------------------------

    def add_path(
        self,
        points: Path,
        stroke: str = "black",
        stroke_width: float = 0.3,
    ) -> None:
        """Add a polyline path to the canvas."""
        if len(points) < 2:
            return
        pts = " ".join(f"{x:.4f},{y:.4f}" for x, y in points)
        self._elements.append(
            f'  <polyline points="{pts}" fill="none" '
            f'stroke="{stroke}" stroke-width="{stroke_width}"/>'
        )

    def add_circle(
        self,
        cx: float,
        cy: float,
        r: float,
        stroke: str = "black",
        stroke_width: float = 0.3,
    ) -> None:
        """Add a circle to the canvas."""
        self._elements.append(
            f'  <circle cx="{cx:.4f}" cy="{cy:.4f}" r="{r:.4f}" '
            f'fill="none" stroke="{stroke}" stroke-width="{stroke_width}"/>'
        )

    # ------------------------------------------------------------------
    # Serialisation
    # ------------------------------------------------------------------

    def to_svg(self) -> str:
        """Render the canvas to an SVG string."""
        header = (
            '<?xml version="1.0" encoding="utf-8"?>\n'
            f'<svg xmlns="http://www.w3.org/2000/svg" '
            f'width="{self.width}{self.unit}" height="{self.height}{self.unit}" '
            f'viewBox="0 0 {self.width} {self.height}">'
        )
        body = "\n".join(self._elements)
        return f"{header}\n{body}\n</svg>"

    def save(self, filepath: str) -> None:
        """Write the SVG to *filepath*."""
        with open(filepath, "w", encoding="utf-8") as fh:
            fh.write(self.to_svg())


# ---------------------------------------------------------------------------
# Pattern generators (pure functions returning paths / geometry)
# ---------------------------------------------------------------------------


def spirograph(
    outer_r: float,
    inner_r: float,
    d: float,
    cx: float,
    cy: float,
    steps: int = 3000,
) -> Path:
    """Return a hypotrochoid (spirograph) curve as a list of points.

    Parameters
    ----------
    outer_r:
        Radius of the fixed outer circle.
    inner_r:
        Radius of the rolling inner circle.
    d:
        Distance from the center of the inner circle to the drawing point.
    cx, cy:
        Center of the figure on the canvas.
    steps:
        Number of line segments used to approximate the curve.
    """
    # Determine the period: the curve closes after lcm(outer_r, inner_r) /
    # inner_r turns of the inner circle, i.e. outer_r / gcd(outer_r, inner_r)
    # full outer turns.
    scale = 1000
    gcd = math.gcd(round(outer_r * scale), round(inner_r * scale))
    full_rotations = round(outer_r * scale) // gcd

    points: Path = []
    for i in range(steps + 1):
        t = 2 * math.pi * full_rotations * i / steps
        x = (outer_r - inner_r) * math.cos(t) + d * math.cos((outer_r - inner_r) / inner_r * t)
        y = (outer_r - inner_r) * math.sin(t) - d * math.sin((outer_r - inner_r) / inner_r * t)
        points.append((cx + x, cy + y))
    return points


def lissajous(
    a: float,
    b: float,
    delta: float,
    width: float,
    height: float,
    cx: float,
    cy: float,
    steps: int = 2000,
) -> Path:
    """Return a Lissajous figure as a list of points.

    Parameters
    ----------
    a, b:
        Frequency parameters for the x and y axes.
    delta:
        Phase offset between the two oscillations.
    width, height:
        Bounding-box dimensions of the figure.
    cx, cy:
        Centre position on the canvas.
    steps:
        Number of line segments.
    """
    points: Path = []
    for i in range(steps + 1):
        t = 2 * math.pi * i / steps
        x = cx + (width / 2) * math.sin(a * t + delta)
        y = cy + (height / 2) * math.sin(b * t)
        points.append((x, y))
    return points


def concentric_circles(
    cx: float,
    cy: float,
    min_r: float,
    max_r: float,
    count: int,
) -> List[Tuple[float, float, float]]:
    """Return a list of ``(cx, cy, r)`` tuples for concentric circles."""
    if count == 1:
        return [(cx, cy, min_r)]
    return [
        (cx, cy, min_r + (max_r - min_r) * i / (count - 1))
        for i in range(count)
    ]


def grid_lines(
    x0: float,
    y0: float,
    x1: float,
    y1: float,
    cols: int,
    rows: int,
) -> List[Path]:
    """Return horizontal and vertical lines forming a grid."""
    paths: List[Path] = []
    for i in range(cols + 1):
        x = x0 + (x1 - x0) * i / cols
        paths.append([(x, y0), (x, y1)])
    for j in range(rows + 1):
        y = y0 + (y1 - y0) * j / rows
        paths.append([(x0, y), (x1, y)])
    return paths


def wave_pattern(
    x0: float,
    y0: float,
    x1: float,
    y1: float,
    rows: int,
    amplitude: float,
    frequency: float,
    steps_per_row: int = 200,
) -> List[Path]:
    """Return a series of sinusoidal wave paths filling a rectangle."""
    paths: List[Path] = []
    for i in range(rows):
        y_base = (
            y0 + (y1 - y0) * i / (rows - 1) if rows > 1 else (y0 + y1) / 2
        )
        path: Path = []
        for j in range(steps_per_row + 1):
            t = j / steps_per_row
            x = x0 + (x1 - x0) * t
            y = y_base + amplitude * math.sin(2 * math.pi * frequency * t)
            path.append((x, y))
        paths.append(path)
    return paths


# ---------------------------------------------------------------------------
# High-level pattern builders
# ---------------------------------------------------------------------------

PATTERNS = {
    "spirograph": "Hypotrochoid (spirograph) curve",
    "lissajous": "Lissajous figure",
    "circles": "Concentric circles",
    "grid": "Grid of lines",
    "waves": "Sine-wave fill pattern",
}


def _generate_spirograph(canvas: SvgCanvas) -> None:
    w, h = canvas.width, canvas.height
    cx, cy = w / 2, h / 2
    outer_r = min(w, h) * 0.40
    inner_r = outer_r / 7
    d = inner_r * 1.2
    canvas.add_path(spirograph(outer_r, inner_r, d, cx, cy))


def _generate_lissajous(canvas: SvgCanvas) -> None:
    w, h = canvas.width, canvas.height
    cx, cy = w / 2, h / 2
    canvas.add_path(lissajous(3, 2, math.pi / 4, w * 0.8, h * 0.8, cx, cy))


def _generate_circles(canvas: SvgCanvas) -> None:
    w, h = canvas.width, canvas.height
    cx, cy = w / 2, h / 2
    max_r = min(w, h) * 0.45
    for ccx, ccy, r in concentric_circles(cx, cy, max_r * 0.05, max_r, 20):
        canvas.add_circle(ccx, ccy, r)


def _generate_grid(canvas: SvgCanvas) -> None:
    w, h = canvas.width, canvas.height
    m = min(w, h) * 0.05
    for path in grid_lines(m, m, w - m, h - m, 20, 20):
        canvas.add_path(path)


def _generate_waves(canvas: SvgCanvas) -> None:
    w, h = canvas.width, canvas.height
    m = min(w, h) * 0.05
    amp = (h - 2 * m) / 40
    for path in wave_pattern(m, m, w - m, h - m, 30, amp, 8):
        canvas.add_path(path)


_GENERATORS = {
    "spirograph": _generate_spirograph,
    "lissajous": _generate_lissajous,
    "circles": _generate_circles,
    "grid": _generate_grid,
    "waves": _generate_waves,
}


# ---------------------------------------------------------------------------
# CLI
# ---------------------------------------------------------------------------


def main(argv: List[str] = None) -> int:
    """Entry point for the command-line interface."""
    parser = argparse.ArgumentParser(
        prog="generate.py",
        description="Generate SVG art files for pen plotters.",
    )
    parser.add_argument(
        "pattern",
        choices=list(PATTERNS),
        metavar="PATTERN",
        help=(
            "Pattern to generate. Choices: "
            + ", ".join(f"{k} ({v})" for k, v in PATTERNS.items())
        ),
    )
    parser.add_argument(
        "-o",
        "--output",
        default=None,
        metavar="FILE",
        help="Output SVG file (default: <pattern>.svg)",
    )
    parser.add_argument(
        "--width",
        type=float,
        default=210.0,
        metavar="MM",
        help="Canvas width in mm (default: 210 — A4 portrait)",
    )
    parser.add_argument(
        "--height",
        type=float,
        default=297.0,
        metavar="MM",
        help="Canvas height in mm (default: 297 — A4 portrait)",
    )

    args = parser.parse_args(argv)
    output = args.output or f"{args.pattern}.svg"

    canvas = SvgCanvas(args.width, args.height)
    _GENERATORS[args.pattern](canvas)
    canvas.save(output)
    print(f"Saved {output}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
