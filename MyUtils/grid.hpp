#ifndef GRID_HPP
#define GRID_HPP

#include <vector>
#include <string>

/* Simple and extensible 2D grid for games or other representations
------------------------------------------------------------------------------------------ */
class Grid {
private:
	std::vector<std::vector<int>> grid;
	std::vector<std::string> symbols;

public:
	int width;
	int height;

	Grid(int w, int h);

	int get_tile(int x, int y);
	bool set_tile(int x, int y, int v);
	std::string get_layout_raw();
	std::string get_layout_formatted();
};

#endif // !GRID_HPP
