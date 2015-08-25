using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatureGraphicsCareer : GraphicsCareerBase {
    List<ObjectBase> livingCreatures = new List<ObjectBase>();
    public List<ObjectBase> LivingCreatures { get { return livingCreatures; } }

    [SerializeField]
    GameObject objectBasePrefab;

    public T PutObject<T>(DungeonAxis axis) where T : ObjectBase
    {
        T objInstance = gameObject.AddChild(objectBasePrefab).AddComponent<T>();
        objInstance.axis = axis;

        objInstance.GetComponent<RectTransform>().anchoredPosition = (Vector2.right * axis.x + Vector2.down * axis.y) * parent.TileUnitSize;

        return objInstance;
    }
}
