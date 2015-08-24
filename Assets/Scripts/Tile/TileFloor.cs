using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TileFloor : TileBase {
    public override void InitGraphic(Graphic graphic)
    {
        graphic.color = Color.white;
    }

    public override bool CanEnterIn(ObjectBase obj)
    {
        return true;
    }

    public override bool CanThroughSlantWise(ObjectBase obj)
    {
        return true;
    }
}
