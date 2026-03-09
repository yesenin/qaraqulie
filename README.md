# plotter-art

A Python script to generate SVG source files for pen plotters.

## Usage

```
python generate.py PATTERN [-o OUTPUT] [--width MM] [--height MM]
```

### Patterns

| Pattern | Description |
|---|---|
| `spirograph` | Hypotrochoid (spirograph) curve |
| `lissajous` | Lissajous figure |
| `circles` | Concentric circles |
| `grid` | Grid of lines |
| `waves` | Sine-wave fill pattern |

### Options

| Option | Default | Description |
|---|---|---|
| `-o FILE` | `<pattern>.svg` | Output SVG file path |
| `--width MM` | `210` | Canvas width in mm (A4 portrait) |
| `--height MM` | `297` | Canvas height in mm (A4 portrait) |

### Examples

```bash
# Generate a spirograph on A4
python generate.py spirograph

# Generate a Lissajous figure with a custom output path
python generate.py lissajous -o my_lissajous.svg

# Generate concentric circles on a square 200×200 mm canvas
python generate.py circles --width 200 --height 200 -o circles.svg
```

All output SVGs use `stroke` with `fill="none"`, which is the standard format
expected by pen plotter software (e.g. Inkscape with the AxiDraw extension,
vpype, or similar tools).

## Requirements

Python 3.8+ — no external dependencies.

## Tests

```bash
python -m pytest tests/
```
