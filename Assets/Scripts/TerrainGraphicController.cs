using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class TerrainGraphicController : MonoBehaviour {
    #region MonoBehaviourSingleton
    public static TerrainGraphicController Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }
    #endregion MonoBehaviourSingleton

    public int VisibleWidth { get; private set; }
    public int VisibleHeight { get; private set; }

    [SerializeField]
    float TileUnitSize = 100f;

    [SerializeField]
    GameObject horizontalTilesPrefab;

    List<List<TileBase>> currentTerrain;

    int terrainWidth, terrainHeight;

	// Use this for initialization
	void Start () {
        Vector2 rootRectSize = transform.parent.GetComponent<RectTransform>().rect.size;
        VisibleWidth = Mathf.FloorToInt(rootRectSize.x / TileUnitSize) + 2;
        VisibleWidth += 1 - VisibleWidth % 2;
        VisibleHeight = Mathf.FloorToInt(rootRectSize.y / TileUnitSize) + 2;
        VisibleHeight += 1 - VisibleHeight % 2;

        var generator = new SimpleTerrain();
        terrainWidth = generator.Width = 20;
        terrainHeight = generator.Height = 15;
        currentTerrain = generator.Generate();

        CreateVoid();
        Reset(PlayerModel.Instance.x, PlayerModel.Instance.y);
	}

    void CreateVoid()
    {
        for (int i = 0; i < VisibleHeight; i++) 
            gameObject.AddChild(horizontalTilesPrefab).GetComponent<HorizontalTiles>().CreateVoidTiles(VisibleWidth, 0);
    }

    void Reset(int centerX, int centerY)
    {
        var horizontals = GetComponentsInChildren<HorizontalTiles>();
        foreach (var horizontal in horizontals) horizontal.ResetTiles(null, 0);

        int leftOffset = Mathf.Max(0, VisibleWidth / 2 - centerX);
        horizontals.SafeSkip(VisibleHeight / 2 - centerY)
            .ZipForEach(currentTerrain.SafeSkip(centerY - VisibleHeight / 2), (horizontalTiles, horizontalTerrain) =>
                horizontalTiles.ResetTiles(horizontalTerrain.SafeSkip(centerX - VisibleWidth / 2), leftOffset));
    }

    public void Move(int deltaX, int deltaY, int previousX, int previousY, Action onTweenFinished)
    {
        var moveTween =
            TweenAnchoredPosition.BeginDelta(gameObject,
            (Vector2.up * deltaY + Vector2.left * deltaX) * TileUnitSize, 0.1f);
        moveTween.OnFinished = () =>
        {
            onTweenFinished();
            HandleGraphicOnMoveFinished(deltaX, deltaY, previousX, previousY);
        };
        moveTween.destroyOnFinished = true;
    }

    void HandleGraphicOnMoveFinished(int deltaX, int deltaY, int previousX, int previousY)
    {
        var horizontals = GetComponentsInChildren<HorizontalTiles>();

        if (deltaX != 0)
        {
            int newTileX = previousX + deltaX + VisibleWidth / 2 * deltaX;
            horizontals.SafeSkip(VisibleHeight / 2 - previousY)
                .ZipForEach(currentTerrain.SafeSkip(previousY - VisibleHeight / 2), (horizontalTiles, horizontalTerrain) =>
                    horizontalTiles.MoveEdgeTile(deltaX > 0, horizontalTerrain.SafeIndexer(previousX + deltaX + VisibleWidth / 2 * deltaX)));
        }

        if (deltaY != 0)
        {
            int leftOffset = Mathf.Max(0, VisibleWidth / 2 - (previousX + deltaX));
            horizontals.TurnEdgeSibling(deltaY > 0)
                .ResetTiles(currentTerrain.SafeIndexer(previousY + deltaY + VisibleHeight / 2 * deltaY).SafeSkip(previousX + deltaX - VisibleWidth / 2),
                    leftOffset);
        }

        GetComponent<RectTransform>().anchoredPosition += (Vector2.down * deltaY + Vector2.right * deltaX) * TileUnitSize;
    }
}
