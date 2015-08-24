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

    public static int VisibleWidth { get; private set; }
    public static int VisibleHeight { get; private set; }

    [SerializeField]
    float TileUnitSize = 100f;

    [SerializeField]
    GameObject horizontalTilesPrefab;

    List<List<TileBase>> currentTerrain;

    int terrainWidth, terrainHeight;

	// Use this for initialization
	void Start () {
        Vector2 rootRectSize = CanvasRoot.Root.GetComponent<RectTransform>().rect.size;
        VisibleWidth = Mathf.FloorToInt(rootRectSize.x / TileUnitSize) + 3;
        VisibleHeight = Mathf.FloorToInt(rootRectSize.y / TileUnitSize) + 3;

        var generator = new SimpleTerrain();
        terrainWidth = generator.Width = 20;
        terrainHeight = generator.Height = 15;
        currentTerrain = generator.Generate();

        Init(PlayerModel.Instance.x, PlayerModel.Instance.y);
	}

    void Init(int centerX, int centerY)
    {
        for (int i = centerY - VisibleHeight / 2; i < centerY + VisibleHeight / 2 + 1; i++)
        {
            GameObject horizontalTiles = UIUtil.AddChild(gameObject, horizontalTilesPrefab);
            if (i < 0 || i > terrainHeight - 1)
            {
                horizontalTiles.GetComponent<HorizontalTiles>().InitHardWall(VisibleWidth);
            }
            else
            {
                int leftOffset = VisibleWidth / 2 - centerX;
                if (leftOffset > 0)
                {
                    horizontalTiles.GetComponent<HorizontalTiles>().Init(currentTerrain[i].Take(VisibleWidth - leftOffset), leftOffset);
                    continue;
                }

                int rightOffset = VisibleWidth / 2 - (terrainWidth - centerX);
                if (rightOffset > 0)
                {
                    horizontalTiles.GetComponent<HorizontalTiles>().Init(currentTerrain[i].Skip(VisibleWidth - rightOffset), -rightOffset);
                    continue;
                }

                horizontalTiles.GetComponent<HorizontalTiles>().Init(currentTerrain[i].Skip(-leftOffset).Take(VisibleWidth), 0);
            }
        }
    }

    public void Move(int horizontalDelta, int verticalDelta, Action onTweenFinished)
    {
        var moveTween =
            TweenAnchoredPosition.BeginDelta(gameObject,
            (Vector2.up * verticalDelta + Vector2.right * horizontalDelta) * TileUnitSize, 0.2f);
        moveTween.OnFinished = () =>
        {
            onTweenFinished();
            HandleGraphicOnMoveFinished(horizontalDelta, verticalDelta);
        };
        moveTween.destroyOnFinished = true;
    }

    void HandleGraphicOnMoveFinished(int horizontalDelta, int verticalDelta)
    {
        if (verticalDelta > 0)
        {
            var movedHorizontal = GetComponentsInChildren<HorizontalTiles>().Last();
            movedHorizontal.transform.SetAsFirstSibling();

            int newLineIndex = PlayerModel.Instance.y - VisibleHeight / 2;
            if (newLineIndex < 0)
            {
                movedHorizontal.InitHardWall(VisibleWidth);
            }
            else
            {
                
            }
        }
    }
}
