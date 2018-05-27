# Simple and extensible 2D grid for games or other representations
class Grid(object):

    # Create 2D grid with given width and height
    def __init__(self, w, h):
        self.grid = [[0 for y in range(h)] for x in range(w)]
        self.symbols = [" ", "#"]

    # Get the tile at given coordinates
    def get_tile(self, x, y):
        if (x < 0 or y < 0) or (x > len(self.grid) or y > len(self.grid[0])):
            return -1
        else:
            return self.grid[x][y]

    # Set the tile's value at given coordinates
    def set_tile(self, x, y, v):
        if (x < 0 or y < 0) or (x > len(self.grid) or y > len(self.grid[0])):
            return False
        else:
            self.grid[x][y] = v
            return True

    # Gets a grid layout of the numerical tile values
    def get_layout_raw(self):
        s = ""
        for y in range(len(self.grid[0])):
            for x in range(len(self.grid)):
                s += str(self.grid[x][y])
            if y != len(self.grid[0]) - 1:
                s += "\n"
        return s

    # Gets a grid layout of the symbols mapped to the numerical tile values
    def get_layout_formatted(self):
        s = ""
        for y in range(len(self.grid[0])):
            for x in range(len(self.grid)):
                if self.grid[x][y] >= 0 and self.grid[x][y] <= len(self.symbols):
                    s += self.symbols[self.grid[x][y]]
                else:
                    s += "?"
            if y != len(self.grid[0]) - 1:
                s += "\n"
        return s

