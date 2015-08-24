using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HorizontalTiles : MonoBehaviour {
    [SerializeField]
    GameObject tilePrefab;

    public void Init(IEnumerable<TileBase> horizontalTiles, int leftOffset)
    {
        foreach (var item in horizontalTiles)
        {
            GameObject tile = UIUtil.AddChild(gameObject, tilePrefab);
            item.InitGraphic(tile.GetComponent<Graphic>());
        }

        if (leftOffset == 0) return;

        int absOffset = Mathf.Abs(leftOffset);
        for (int i = 0; i < absOffset; i++)
        {
            GameObject tile = UIUtil.AddChild(gameObject, tilePrefab);
            if (leftOffset > 0) tile.transform.SetAsFirstSibling();
            TileHardWall.OuterWall.InitGraphic(tile.GetComponent<Graphic>());
        }

    }

    public void InitHardWall(int width)
    {
        for (int i = 0; i < width; i++)
        {
            GameObject tile = UIUtil.AddChild(gameObject, tilePrefab);
            TileHardWall.OuterWall.InitGraphic(tile.GetComponent<Graphic>());
        }
    }
}
