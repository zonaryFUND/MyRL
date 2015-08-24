using UnityEngine;
using UnityEngine.UI;

public static class UIUtil {
    public static GameObject AddChild(GameObject parent, GameObject prefab)
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
}
