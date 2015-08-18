using UnityEngine;
using System.Collections;
using System;

public class TweenPosition : TweenBase<Vector3> {
    protected override void TweenProgress(float progressRate)
    {
        transform.localPosition = (from - to) * progressRate + from;
    }

    protected override Vector3 Initial()
    {
        return transform.localPosition;
    }
}
