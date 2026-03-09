import random

def create_svg(width: int, height: int, lines: list) -> str:
    svg_lines = []
    svg_lines.append('<svg xmlns="http://www.w3.org/2000/svg"')
    svg_lines.append(f'\twidth="{width}mm" height="{height}mm"')
    svg_lines.append(f'\tviewBox="0 0 {width} {height}">')
    svg_lines.append('<rect width="100%" height="100%" fill="pink"/>')
    for line in lines:
        svg_lines.append(line)
    svg_lines.append('</svg>')
    return '\n'.join(svg_lines)

def init_grid(width_cells: int, height_cells: int, x_step: float, y_step: float) -> list:
    grid = []
    x = MARGIN_MM + CANVAS_MARGIN_X
    y = MARGIN_MM + CANVAS_MARGIN_Y
    for y in range(height_cells + 1):
        row = []
        for x in range(width_cells + 1):
            row.append((x * x_step + MARGIN_MM + CANVAS_MARGIN_X, y * y_step + MARGIN_MM + CANVAS_MARGIN_Y))
            x += 1
        grid.append(row)
        x = 0
        y += 1
    return grid

def swaggle_grid(grid: list) -> list:
    result = []
    limit = MARGIN_MM - 2.0
    big_limit = 20.0
    r_i = 0
    c_i = 0
    for row in grid:
        new_row = []
        for cell in row:
            x, y = cell
            if (r_i == 0 or r_i == len(grid) - 1 or c_i == 0 or c_i == len(row) - 1):
                x += random.uniform(-limit, limit)
                y += random.uniform(-limit, limit)
            else:
                x += random.uniform(-big_limit, big_limit)
                y += random.uniform(-big_limit, big_limit)
            new_row.append((x, y))
        result.append(new_row)
    return result

def draw_grid(grid: list) -> list:
    grid_lines = []
    pts = []
    grid_lines.append('<g stroke="black" stroke-width="0.5">')
    r_i = 0
    dict_rows = {}
    for row in grid:
        for cell in row:
            x, y = cell
            pts.append(f'{x:.3f},{y:.3f}')
            if (r_i < len(grid) - 1):
                bottom_cell = grid[r_i + 1][row.index(cell)]
                x2, y2 = bottom_cell
                for a in range(PARTS):
                    t = a / PARTS
                    p_x = x + t * (x2 - x)
                    p_y = y + t * (y2 - y)
                    row_key = r_i * PARTS + a
                    if row_key not in dict_rows:
                        dict_rows[row_key] = []
                    dict_rows[row_key].append((p_x, p_y))
        #grid_lines.append(f'<polyline points="{" ".join(pts)}" fill="none"/>')
        r_i += 1
    #grid_lines.append('</g>')
    #grid_lines.append('<g stroke="red" stroke-width="0.4">')
    for row_key, row_pts in dict_rows.items():
        t_pts = []
        for x, y in row_pts:
            print(f'{row_key}:{x:.3f},{y:.3f}')
            t_pts.append(f'{x:.3f},{y:.3f}')
        print('---')
        grid_lines.append(f'<polyline points="{" ".join(t_pts)}" fill="none"/>')
    grid_lines.append('</g>')
    return '\n'.join(grid_lines)

WIDTH_MM = 297.0
HEIGHT_MM = 210.0

MARGIN_MM = 10.0

CANVAS_MARGIN_X = 20
CANVAS_MARGIN_Y = 30

PARTS = 12

if __name__ == '__main__':
    CANVAS_WIDTH_MM = WIDTH_MM - 2 * (MARGIN_MM + CANVAS_MARGIN_X)
    CANVAS_HEIGHT_MM = HEIGHT_MM - 2 * (MARGIN_MM + CANVAS_MARGIN_Y)
    grid_width_cells = 15
    grid_height_cells = 10
    x_step = CANVAS_WIDTH_MM / grid_width_cells
    y_step = CANVAS_HEIGHT_MM / grid_height_cells
    grid = init_grid(grid_width_cells, grid_height_cells, x_step, y_step)
    grid = swaggle_grid(grid)
    g_grid = draw_grid(grid)
    svg_content = create_svg(WIDTH_MM, HEIGHT_MM, [g_grid])
    with open('art.svg', 'w', encoding='utf-8') as f:
        f.write(svg_content)
    print('Done.')
