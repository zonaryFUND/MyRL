using UnityEngine;
using System.Collections;

public class KeyboardListener : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            PlayerModel.Instance.Move((Input.GetKey(KeyCode.LeftArrow) ? 1 : 0) + (Input.GetKey(KeyCode.RightArrow) ? -1 : 0),
            (Input.GetKey(KeyCode.DownArrow) ? 1 : 0) + (Input.GetKey(KeyCode.UpArrow) ? -1 : 0));
        }

    }
}
