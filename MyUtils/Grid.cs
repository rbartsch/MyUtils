/// <summary>
/// Simple and extendible 2D grid for games or other representions.
/// </summary>
public class Grid {
    private int[,] grid;
    private readonly char[] symbols = new char[] { ' ', '#' };

    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Create 2D grid with given width and height
    /// </summary>
    /// <param name="w">width</param>
    /// <param name="h">height</param>
    public Grid(int w, int h) {
        grid = new int[w, h];
    }

    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Get the tile at given coordinates
    /// </summary>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    /// <returns></returns>
    public int GetTile(int x, int y) {
        if ((x < 0 || y < 0) || (x > grid.GetUpperBound(0) || y > grid.GetUpperBound(1))) {
            return -1;
        }
        else {
            return grid[x, y];
        }
    }

    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Set the tile's value at given coordinates
    /// </summary>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    /// <param name="v">tile value</param>
    /// <returns></returns>
    public bool SetTile(int x, int y, int v) {
        if ((x < 0 || y < 0) || (x > grid.GetUpperBound(0) || y > grid.GetUpperBound(1))) {
            return false;
        }
        else {
            grid[x, y] = v;
            return true;
        }
    }

    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Gets a grid layout of the numerical tile values
    /// </summary>
    /// <returns>string</returns>
    public string GetLayoutRaw() {
        StringBuilder sb = new StringBuilder();
        int b0 = grid.GetUpperBound(0);
        int b1 = grid.GetUpperBound(1);
        for(int y = 0; y <= b1; y++) {
            for(int x = 0; x <= b0; x++) {
                sb.Append(grid[x, y]);
            }
            if(y != b1) {
                sb.Append("\n");
            }
        }
   
        return sb.ToString();
    }
    
    // ------------------------------------------------------------------------------------------
    /// <summary>
    /// Gets a grid layout of the symbols mapped to the numerical tile values
    /// </summary>
    /// <returns></returns>
    public string GetLayoutFormatted() {
        StringBuilder sb = new StringBuilder();
        int b0 = grid.GetUpperBound(0);
        int b1 = grid.GetUpperBound(1);
        for(int y = 0; y <= b1; y++) {
            for(int x = 0; x <= b0; x++) {
                if(grid[x,y] >= 0 && grid[x,y] <= symbols.Length - 1) {
                    sb.Append(symbols[grid[x, y]]);
                }
                else {
                    sb.Append("?");
                }
            }
            if(y != b1) {
                sb.Append("\n");
            }
        }
        return sb.ToString();
    }
}
