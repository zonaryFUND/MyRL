using UnityEngine;
using System.Collections;

public class DungeonInputMediator {
    public bool IsEnabled { get; set; }

    public DungeonInputMediator()
    {
        IsEnabled = true;
    }

    [SerializeField]
    float slantWaitTime = 0.01f;

    float slantWaitStart = 0f;

    public void DirectionKey(bool up, bool down, bool left, bool right)
    {
        if (!IsEnabled) return;

        int vertical = (up ? -1 : 0) + (down ? 1 : 0);
        int horizontal = (left ? -1 : 0) + (right ? 1 : 0);

        if (vertical * horizontal == 0 && Mathf.Abs(vertical) + Mathf.Abs(horizontal) != 0)
        {
            if (slantWaitStart == 0f)
            {
                slantWaitStart = Time.time;
                return;
            }
            if (Time.time - slantWaitStart < slantWaitTime) return;
        }
        else
        {
            slantWaitStart = 0f;
        }
        

    }
}
