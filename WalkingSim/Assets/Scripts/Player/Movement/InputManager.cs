using UnityEngine;
public class InputManager : MonoBehaviour
{
    #region componants
    //public
    [Tooltip("these bools are getting used in other scripts")]
    public bool forwardPressed, backwardsPressed, leftPressed, rightPressed, runPressed, leftClick, rightClick, taunt, equip, spacebar;
    #endregion
    void Update()
    {
        forwardPressed = Input.GetButton("W");
        backwardsPressed = Input.GetButton("S");
        leftPressed = Input.GetButton("A");
        rightPressed = Input.GetButton("D");
        spacebar = Input.GetButtonDown("Jump");
        runPressed = Input.GetButton("Run");
        leftClick = Input.GetButtonDown("Fire1");
        rightClick = Input.GetButtonDown("Fire2");
    }
}