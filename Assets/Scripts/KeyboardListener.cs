using UnityEngine;
using System.Collections;

public class KeyboardListener : MonoBehaviour {
    [SerializeField]
    DungeonInputMediator inputMediator;

    // Update is called once per frame
    void Update()
    {
        inputMediator.DirectionKey(Input.GetKeyDown(KeyCode.UpArrow), Input.GetKeyDown(KeyCode.DownArrow),
            Input.GetKeyDown(KeyCode.LeftArrow), Input.GetKeyDown(KeyCode.RightArrow));
    }
}
