import cairosvg
from numpy import sqrt

WIDTH_MM = 297.0
HEIGHT_MM = 210.0

MARGIN_MM = 10.0

def rotate_n_times(line: str, n: int) -> str:
    for _ in range(n):
        line = rotate_line(line)
    return line

def rotate_line(line: str) -> str:
    # line is a string like 'u', 'r', 'd', 'l'
    if line == 'u':
        return 'l'
    elif line == 'l':
        return 'd'
    elif line == 'd':
        return 'r'
    elif line == 'r':
        return 'u'
    else:
        raise ValueError(f'Invalid line: {line}')

def init_dragon(limit: int, level: int, lines: list):
    if level == limit:
        return lines
    if level == 0:
        lines.append('u')
    else:
        for i in range(len(lines) - 1, -1, -1):
            lines.append(rotate_line(lines[i]))
    return init_dragon(limit, level + 1, lines)

def draw_dragon(lines, side: float) -> list:
    pts = []
    last_x = 222.0
    last_y = 76.0
    pts.append(f'{last_x:.3f},{last_y:.3f}')
    for line in lines:
        if line == 'u':
            new_x = last_x - side
            new_y = last_y
        elif line == 'r':
            new_x = last_x
            new_y = last_y + side
        elif line == 'd':
            new_x = last_x + side
            new_y = last_y
        elif line == 'l':
            new_x = last_x
            new_y = last_y - side
        else:
            print(line)
            raise ValueError(f'Invalid line: {line}')
        pts.append(f'{new_x:.3f},{new_y:.3f}')
        last_x, last_y = new_x, new_y
    dragon_lines = []
    dragon_lines.append('<g fill="none" stroke="black" stroke-width="0.1">')
    dragon_lines.append(f'<polyline points="{" ".join(pts)}"/>')
    dragon_lines.append('</g>')
    return ''.join(dragon_lines)

def create_svg(width: int, height: int, lines: list) -> str:
    svg_lines = []
    svg_lines.append('<svg xmlns="http://www.w3.org/2000/svg"')
    svg_lines.append(f'\twidth="{width}mm" height="{height}mm"')
    svg_lines.append(f'\tviewBox="0 0 {width} {height}">')
    svg_lines.append('<rect width="100%" height="100%" fill="white"/>')

    svg_lines.extend(lines)

    svg_lines.append('</svg>')
    return '\n'.join(svg_lines)

if __name__ == '__main__':
    dragon = init_dragon(17, 0, [])
    dragon_lines = draw_dragon(dragon, 0.7)
    svg_content = create_svg(WIDTH_MM, HEIGHT_MM, [dragon_lines])
    with open('art.svg', 'w', encoding='utf-8') as f:
        f.write(svg_content)
    cairosvg.svg2png(url="art.svg", write_to=f"art.png")
    print('Done.')