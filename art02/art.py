import cairosvg

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

def create_squares(side: float) -> str:
    squares = []
    for i in range(PARTS):
        x = MARGIN_MM + CANVAS_MARGIN_X + (WIDTH_MM - 2 * MARGIN_MM) / PARTS * i + 2
        y = MARGIN_MM + CANVAS_MARGIN_Y + 2
        w = side - 4
        h = side - 4
        squares.append(f'<rect x="{x:.3f}" y="{y:.3f}" width="{w:.3f}" height="{h:.3f}" fill="none" stroke="black" stroke-width="0.3"/>')
    return ''.join(squares)

WIDTH_MM = 297.0
HEIGHT_MM = 210.0

MARGIN_MM = 10.0

CANVAS_MARGIN_X = 0
CANVAS_MARGIN_Y = 30

PARTS = 20

if __name__ == '__main__':
    CANVAS_WIDTH_MM = WIDTH_MM - 2 * (MARGIN_MM + CANVAS_MARGIN_X)
    squares = create_squares(CANVAS_WIDTH_MM/PARTS)
    svg_content = create_svg(WIDTH_MM, HEIGHT_MM, [squares])
    with open('art.svg', 'w', encoding='utf-8') as f:
        f.write(svg_content)
    cairosvg.svg2png(url="art.svg", write_to=f"art.png")
    print('Done.')
