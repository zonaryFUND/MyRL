using UnityEngine;
using System.Collections.Generic;

public class SimpleTerrain : ITerrainGenerator {
    public int Height, Width;

    public List<List<TileBase>> Generate()
    {
        var terrain = new List<List<TileBase>>();
        for (int i = 0; i < Height; i++)
        {
            var vertical = new List<TileBase>();
            for (int j = 0; j < Width; j++)
            {
                if (i * (i - Height + 1) * j * (j - Width + 1) == 0)
                {
                    vertical.Add(new TileHardWall());
                }
                else
                {
                    vertical.Add(new TileFloor());
                }
            }
            terrain.Add(vertical);
        }

        return terrain;
    }
}
