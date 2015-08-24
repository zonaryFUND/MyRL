using UnityEngine;
using System.Collections;

public class PlayerModel : MonoBehaviour {
    #region MonoBehaviourSingleton
    public static PlayerModel Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }
    #endregion MonoBehaviourSingleton
    public int x = 2, y = 2;


    bool onMoving = false;

    public void Move(int horizontal, int vertical)
    {
        if (onMoving) return;

        x += horizontal;
        y += vertical;
        onMoving = true;
        TerrainGraphicController.Instance.Move(horizontal, vertical, OnMoveTweenFinished);
    }

    void OnMoveTweenFinished()
    {
        onMoving = false;
    }

}
