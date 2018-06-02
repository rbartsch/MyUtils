/// <summary>
/// Simple and extendible 2D grid for games or other representions.
/// Always has a base layer that starts at 0, layers are above.
/// Base layer can represent the "floor" including walls, 
/// while above layers which replicate the size of the base layer
/// can represent enemies and anything else.
/// </summary>
public class Grid {
    private Dictionary<int, int[,]> layers = new Dictionary<int, int[,]>();
    private Dictionary<int, char[]> layersSymbols = new Dictionary<int, char[]>();

    private readonly int w;
    private readonly int h;


    /// <summary>
    /// Create 2D grid with given width and height, always starts
    /// with a base layer at 0 automatically.
    /// </summary>
    /// <param name="w">width</param>
    /// <param name="h">height</param>
    /// <param name="l">additional layers</param>
    public Grid(int w, int h, int l = 0) {
        this.w = w;
        this.h = h;

        layers.Add(0, new int[w, h]);

        for(int i = 0; i < l; i++) {
            layers.Add(layers.Count, new int[w, h]);
        }
    }


    /// <summary>
    /// Adds a new layer in ascending order
    /// </summary>
    public void AddLayer() {
        layers.Add(layers.Count, new int[w, h]);
    }

    // TODO(rbartsch): Remove add handle symbols/textures in world grid
    /// <summary>
    /// Add an array of symbols that correspond to the grid tile values
    /// in ascending order starting from 0. i.e grid tile value 0 = s[0]
    /// </summary>
    /// <param name="l">layer</param>
    /// <param name="s">symbols</param>
    public void AddSymbolsForLayer(int l, char[] s) {
        layersSymbols.Add(l, s);
    }


    /// <summary>
    /// Get the tile at given coordinates
    /// </summary>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    /// <returns></returns>
    public int GetTile(int x, int y, int l = 0) {
        if (l < 0 || l > layers.Count) {
            return -1;
        }

        if ((x < 0 || y < 0) || (x > layers[l].GetUpperBound(0) || y > layers[l].GetUpperBound(1))) {
            return -1;
        }
        else {
            return layers[l][x, y];
        }
    }


    /// <summary>
    /// Set the tile's value at given coordinates
    /// </summary>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    /// <param name="v">tile value</param>
    /// <returns></returns>
    public bool SetTile(int x, int y, int v, int l = 0) {
        if(l < 0 || l > layers.Count) {
            return false;
        }

        if ((x < 0 || y < 0) || (x > layers[l].GetUpperBound(0) || y > layers[l].GetUpperBound(1))) {
            return false;
        }
        else {
            layers[l][x, y] = v;
            return true;
        }
    }


    /// <summary>
    /// Gets a grid layout of the numerical tile values
    /// </summary>
    /// <returns>string</returns>
    public string GetLayoutRaw(int l = 0) {
        if (l < 0 || l > layers.Count) {
            return "Unknown layer";
        }

        StringBuilder sb = new StringBuilder();
        int b0 = layers[l].GetUpperBound(0);
        int b1 = layers[l].GetUpperBound(1);
        for (int y = 0; y <= b1; y++) {
            for (int x = 0; x <= b0; x++) {
                sb.Append(layers[l][x, y]);
            }
            if (y != b1) {
                sb.Append("\n");
            }
        }

        return sb.ToString();
    }


    // TODO(rbartsch): Remove add handle symbols/textures in world grid
    /// <summary>
    /// Gets a grid layout of the symbols mapped to the numerical tile values
    /// </summary>
    /// <returns></returns>
    public string GetLayoutFormatted(int l = 0) {
        if (l < 0 || l > layers.Count) {
            return "Unknown layer";
        }

        StringBuilder sb = new StringBuilder();
        int b0 = layers[l].GetUpperBound(0);
        int b1 = layers[l].GetUpperBound(1);
        for (int y = 0; y <= b1; y++) {
            for (int x = 0; x <= b0; x++) {
                if (layers[l][x, y] >= 0 && layers[l][x, y] <= layersSymbols[l].Length - 1) {
                    sb.Append(layersSymbols[l][layers[l][x, y]]);
                }
                else {
                    sb.Append("?");
                }
            }
            if (y != b1) {
                sb.Append("\n");
            }
        }
        return sb.ToString();
    }
}
