using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class TerrainGraphicCareer : GraphicsCareerBase {
    [SerializeField]
    GameObject horizontalTilesPrefab;

	// Use this for initialization
	void Start () {
        CreateVoid();
	}

    void CreateVoid()
    {
        for (int i = 0; i < parent.VisibleHeight; i++) 
            gameObject.AddChild(horizontalTilesPrefab).GetComponent<HorizontalTiles>().CreateVoidTiles(parent.VisibleWidth, 0);
    }

    public void Reset(DungeonAxis playerInitial, List<List<TileBase>> terrain)
    {
        var horizontals = GetComponentsInChildren<HorizontalTiles>();
        foreach (var horizontal in horizontals) horizontal.ResetTiles(null, 0);

        int leftX = playerInitial.x - parent.VisibleWidth / 2;
        int topY = playerInitial.y - parent.VisibleHeight / 2;
        horizontals.SafeSkip(leftX)
            .ZipForEach(terrain.SafeSkip(topY), (horizontalTile, horizontalTerrain) =>
                horizontalTile.ResetTiles(horizontalTerrain.SafeSkip(leftX), Mathf.Max(0, -leftX)));
    }
    
    public void Move(DungeonAxis delta, DungeonAxis playerPrevious, List<List<TileBase>> terrain,
        float tweenDuration, Action onTweenFinished)
    {
        var moveTween = 
            TweenAnchoredPosition
            .BeginDelta(gameObject, (Vector2.up * delta.y + Vector2.left * delta.x) * parent.TileUnitSize, tweenDuration);
        moveTween.OnFinished = () =>
        {
            onTweenFinished();
            HandleGraphicOnMoveFinished(delta, playerPrevious, terrain);
        };
        moveTween.destroyOnFinished = true;
    }

    void HandleGraphicOnMoveFinished(DungeonAxis delta, DungeonAxis previous, List<List<TileBase>> terrain)
    {
        var horizontals = GetComponentsInChildren<HorizontalTiles>();
        
        if (delta.x != 0)
        {
            int newTileX = previous.x + delta.x + parent.VisibleWidth / 2 * delta.x;
            horizontals.SafeSkip(parent.VisibleHeight / 2 - previous.y)
                .ZipForEach(terrain.SafeSkip(previous.y - parent.VisibleHeight / 2), (horizontalTiles, horizontalTerrain) =>
                    horizontalTiles.MoveEdgeTile(delta.x > 0, horizontalTerrain.SafeIndexer(newTileX)));
        }

        if (delta.y != 0)
        {
            int leftOffset = Mathf.Max(0, parent.VisibleWidth / 2 - (previous.x + delta.x));
            horizontals.TurnEdgeSibling(delta.y > 0)
                .ResetTiles(terrain.SafeIndexer(previous.y + delta.y + parent.VisibleHeight / 2 * delta.y).SafeSkip(previous.x + delta.x - parent.VisibleWidth / 2),
                    leftOffset);
        }

        GetComponent<RectTransform>().anchoredPosition += (Vector2.down * delta.y + Vector2.right * delta.x) * parent.TileUnitSize;
    }
}
