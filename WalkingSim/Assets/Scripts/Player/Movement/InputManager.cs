using UnityEngine;
public class InputManager : MonoBehaviour
{
    //public
    public bool forwardPressed, backwardsPressed, leftPressed, rightPressed, runPressed, leftClick, rightClick, taunt, equip, spacebar;
    void Update()
    {
        forwardPressed = Input.GetKey("w");
        backwardsPressed = Input.GetKey("s");
        leftPressed = Input.GetKey("a");
        rightPressed = Input.GetKey("d");
        spacebar = Input.GetKeyDown("space");
        runPressed = Input.GetKey("left shift");
        leftClick = Input.GetKeyDown(KeyCode.Mouse0);
        rightClick = Input.GetKey(KeyCode.Mouse1);
    }
}