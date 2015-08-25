using UnityEngine;

public class GraphicsParent : MonoBehaviour {
    [SerializeField]
    CreatureGraphicsCareer creatureGraphicsCareer;
    public CreatureGraphicsCareer Creatures { get { return creatureGraphicsCareer; } }

    [SerializeField]
    TerrainGraphicCareer terrainGraphicsCareer;
    public TerrainGraphicCareer Terrain { get { return terrainGraphicsCareer; } }

    [SerializeField]
    float tileUnitSize = 100f;
    public float TileUnitSize { get { return tileUnitSize; } }

    public int VisibleWidth { get; private set; }
    public int VisibleHeight { get; private set; }

    void Start()
    {
        Vector2 rootRectSize = transform.parent.GetComponent<RectTransform>().rect.size;
        VisibleWidth = Mathf.FloorToInt(rootRectSize.x / TileUnitSize) + 2;
        VisibleWidth += 1 - VisibleWidth % 2;
        VisibleHeight = Mathf.FloorToInt(rootRectSize.y / TileUnitSize) + 2;
        VisibleHeight += 1 - VisibleHeight % 2;

        creatureGraphicsCareer.Parent = terrainGraphicsCareer.Parent = this;
    }

    public void PlayerGraphicsMove(DungeonAxis delta, DungeonAxis playerPrevious)
    {

    }
}
