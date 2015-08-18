using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TweenAnchoredPosition : TweenBase<Vector2> {
    public static TweenAnchoredPosition Begin(GameObject gameObject, Vector2 to, float duration)
    {
        return TweenBase<Vector2>.Begin<TweenAnchoredPosition>(gameObject, to, duration);
    }

    public static TweenAnchoredPosition BeginDelta(GameObject gameObject, Vector2 delta, float duration)
    {
        var retTween = Begin(gameObject, Vector2.zero, duration);
        retTween.to = retTween.Initial() + delta;
        return retTween;
    }


    protected override void TweenProgress(float progressRate)
    {
        GetComponent<RectTransform>().anchoredPosition = (to - from) * progressRate + from;
    }

    protected override Vector2 Initial()
    {
        return GetComponent<RectTransform>().anchoredPosition;
    }
}
