using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TileHardWall : TileBase {
    public override void InitGraphic(Graphic graphic)
    {
        graphic.color = Color.blue;
    }

    public override bool CanEnterIn(ObjectBase obj)
    {
        return false;
    }

    public override bool CanThroughSlantWise(ObjectBase obj)
    {
        return false;
    }

    static TileHardWall outerWall = new TileHardWall();
    public static TileHardWall OuterWall
    {
        get { return outerWall; }
    }
}
