#include "grid.hpp"

/* Create 2D grid with given width and height
------------------------------------------------------------------------------------------ */
Grid::Grid(int w, int h) {
	width = w;
	height = h;
	grid.resize(w, std::vector<int>(h, 0));
	symbols = { " ", "#" };
}

/* Get the tile at given coordinates
------------------------------------------------------------------------------------------ */
int Grid::get_tile(int x, int y) {
	if ((x < 0 || y < 0) || (x > width - 1 || y > height - 1)) {
		return -1;
	}
	else {
		return grid[x][y];
	}
}

/* Set the tile's value at given coordinates
------------------------------------------------------------------------------------------ */
bool Grid::set_tile(int x, int y, int v) {
	if ((x < 0 || y < 0) || (x > width - 1 || y > height - 1)) {
		return false;
	}
	else {
		grid[x][y] = v;
		return true;
	}
}

/* Gets a grid layout of the numerical tile values
------------------------------------------------------------------------------------------ */
std::string Grid::get_layout_raw() {
	std::string s = "";

	for (int y = 0; y <= height - 1; y++) {
		for (int x = 0; x <= width - 1; x++) {
			s += std::to_string(grid[x][y]);
		}

		if (y != height - 1) {
			s += "\n";
		}
	}

	return s;
}

/* Gets a grid layout of the symbols mapped to the numerical tile values
------------------------------------------------------------------------------------------ */
std::string Grid::get_layout_formatted() {
	std::string s = "";

	for (int y = 0; y <= height - 1; y++) {
		for (int x = 0; x <= width - 1; x++) {
			if (grid[x][y] >= 0 && grid[x][y] <= symbols.size() - 1) {
				s += symbols[grid[x][y]];
			}
			else {
				s += "?";
			}
		}

		if (y != height - 1) {
			s += "\n";
		}
	}

	return s;
}
