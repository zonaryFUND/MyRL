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

        onMoving = true;
        //TerrainGraphicController.Instance.Move(horizontal, vertical, x, y, OnMoveTweenFinished);
        x += horizontal;
        y += vertical;
    }

    void OnMoveTweenFinished()
    {
        onMoving = false;
    }

}
