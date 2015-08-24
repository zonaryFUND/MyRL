using System.Collections.Generic;

public interface ITerrainGenerator {
    List<List<TileBase>> Generate();
}
