using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CanvasRoot : MonoBehaviour {
    static readonly string DefaultRootKey = "CanvasRoot_Default";

    static Dictionary<string, GameObject> roots = new Dictionary<string, GameObject>();

    [SerializeField]
    string rootKey;

    void Awake()
    {
        if (string.IsNullOrEmpty(rootKey))
        {
            if (roots.Any(item => item.Key == DefaultRootKey)) throw new UnityException("There are two or more root with no RootTag!");

            roots.Add(DefaultRootKey, gameObject);
        }
        else
        {
            roots.Add(rootKey, gameObject);
        }
    }

    public static GameObject Root
    {
        get { return roots[DefaultRootKey]; }
    }

    public static GameObject RootWithKey(string key)
    {
        return roots[key];
    }
}
