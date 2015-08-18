using UnityEngine;
using System.Collections;

public class TerrainMover : MonoBehaviour {
    #region MonoBehaviourSingleton
    public static TerrainMover Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }
    #endregion MonoBehaviourSingleton
    [SerializeField]
    float tileSize;

    bool onMoving = false;

    public void Move(int horizontal, int vertical)
    {
        if (onMoving) return;

        onMoving = true;
        var moveTween = 
            TweenAnchoredPosition.BeginDelta(gameObject,
            (Vector2.up * vertical + Vector2.right * horizontal) * tileSize,
            0.2f);
        moveTween.OnFinished = () => onMoving = false;
        moveTween.destroyOnFinished = true;
    }
}
