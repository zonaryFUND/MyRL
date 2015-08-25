using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public static class UIUtil {
    public static GameObject AddChild(this GameObject parent, GameObject prefab)
    {
        GameObject gameObject = GameObject.Instantiate(prefab);
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.localScale = prefab.transform.localScale;

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            RectTransform prefabRectTransform = prefab.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = prefabRectTransform.anchoredPosition;
            rectTransform.sizeDelta = prefabRectTransform.sizeDelta;
        }
        else
        {
            gameObject.transform.localPosition = prefab.transform.localPosition;
        }

        return gameObject;
    }

    public static T TurnEdgeSibling<T>(this IEnumerable<T> components, bool firstToLast) where T : Component
    {
        T turned;
        if (firstToLast)
        {
            turned = components.First();
            turned.transform.SetAsLastSibling();
        }
        else
        {
            turned = components.Last();
            turned.transform.SetAsFirstSibling();
        }

        return turned;
    }
}
