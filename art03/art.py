import cairosvg
import random

def create_svg(width: int, height: int, lines: list) -> str:
    svg_lines = []
    svg_lines.append('<svg xmlns="http://www.w3.org/2000/svg"')
    svg_lines.append(f'\twidth="{width}mm" height="{height}mm"')
    svg_lines.append(f'\tviewBox="0 0 {width} {height}">')
    svg_lines.append('<rect width="100%" height="100%" fill="white"/>')
    for line in lines:
        svg_lines.append(line)
    svg_lines.append('</svg>')
    return '\n'.join(svg_lines)

def create_circles() -> str:
    squares = []

    x1 = 50
    y1 = 60
    r1 = 40
    squares.append(f'<circle cx="{x1:.3f}" cy="{y1:.3f}" r="{r1}" fill="none" stroke="black" stroke-width="0.3"/>')

    x2 = 130
    y2 = 110
    r2 = 30
    squares.append(f'<circle cx="{x2:.3f}" cy="{y2:.3f}" r="{r2}" fill="none" stroke="red" stroke-width="0.3"/>')

    return ''.join(squares)

WIDTH_MM = 297.0
HEIGHT_MM = 210.0

MARGIN_MM = 10.0

CANVAS_MARGIN_X = 0
CANVAS_MARGIN_Y = 30

PARTS = 20

if __name__ == '__main__':
    CANVAS_WIDTH_MM = WIDTH_MM - 2 * (MARGIN_MM + CANVAS_MARGIN_X)
    circles = create_circles()
    svg_content = create_svg(WIDTH_MM, HEIGHT_MM, [circles])
    with open('art.svg', 'w', encoding='utf-8') as f:
        f.write(svg_content)
    cairosvg.svg2png(url="art.svg", write_to=f"art.png")
    print('Done.')
