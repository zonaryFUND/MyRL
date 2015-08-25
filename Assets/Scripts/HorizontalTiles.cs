using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class HorizontalTiles : MonoBehaviour {
    [SerializeField]
    GameObject tilePrefab;


    public void CreateVoidTiles(int width, int leftOffset)
    {
        for (int i = 0; i < width; i++) gameObject.AddChild(tilePrefab);
    }


    public void ResetTiles(IEnumerable<TileBase> horizontalTiles, int leftOffset)
    {
        ResetHardWall();
        if (horizontalTiles != null)
            horizontalTiles.ZipForEach(GetComponentsInChildren<Graphic>().Skip(leftOffset), (tile, graphic) => tile.InitGraphic(graphic));
    }

    void ResetHardWall()
    {
        foreach (var graphic in GetComponentsInChildren<Graphic>()) TileHardWall.OuterWall.InitGraphic(graphic);
    }

    public void MoveEdgeTile(bool leftToRight, TileBase newTile)
    {
        (newTile ?? TileHardWall.OuterWall).InitGraphic(GetComponentsInChildren<Graphic>().TurnEdgeSibling(leftToRight));
    }
}
