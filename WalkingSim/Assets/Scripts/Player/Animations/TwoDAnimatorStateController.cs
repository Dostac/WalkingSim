using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TwoDAnimatorStateController : MonoBehaviour
{
    #region public componants
    [Header("Script componants")]
    [Tooltip("the input manager script that is on the player")]
    public InputManager im;
    [Tooltip("the animator")]
    public Animator anim;
    [Header("Animator Values")]
    [Tooltip("the speed for the axees value to go up")]
    public float acceleration=2f;
    [Tooltip("the speed for the axees value to go down")]
    public float decelaration = 2f;
    [Space(2)]
    [Tooltip("max velocity to walk")]
    public float maxWalkVelocity=0.5f;
    [Tooltip("max velocity to run")]
    public float maxRunVelocity = 2.0f;
    #endregion
    #region private componants
    //private
    private float velocitiyZ = 0.0f;
    private float velocitiyX = 0.0f;
    #endregion
    #region animation velocity check/set
    void ChangeVelocity(bool forwardPressed, bool backwardsPressed, bool leftPressed ,bool rightPressed,bool runPressed,float currentMaxVelocity)
    {
        if (forwardPressed && velocitiyZ < currentMaxVelocity)
        {
            velocitiyZ += Time.deltaTime * acceleration;
        }
        if (backwardsPressed && velocitiyZ > -currentMaxVelocity)
        {
            velocitiyZ -= Time.deltaTime * acceleration;
        }
        if (leftPressed && velocitiyX > -currentMaxVelocity)
        {
            velocitiyX -= Time.deltaTime * acceleration;
        }
        if (rightPressed && velocitiyX < currentMaxVelocity)
        {
            velocitiyX += Time.deltaTime * acceleration;
        }
        if (!forwardPressed && velocitiyZ > 0.0f)
        {
            velocitiyZ -= Time.deltaTime * decelaration;
        }
        if (!backwardsPressed && velocitiyZ < 0.0f)
        {
            velocitiyZ += Time.deltaTime * decelaration;
        }
        if (!leftPressed && velocitiyX < 0.0f)
        {
            velocitiyX += Time.deltaTime * decelaration;
        }
        if (!rightPressed && velocitiyX > 0.0f)
        {
            velocitiyX -= Time.deltaTime * decelaration;
        }
    }
    void  LockOrRessetVelocity(bool forwardPressed, bool backwardsPressed, bool leftPressed,bool rightPressed,bool runPressed,float currentMaxVelocity)
    {
        if (!leftPressed && !rightPressed && velocitiyX != 0.0f && (velocitiyX > maxRunVelocity && velocitiyX < maxRunVelocity))
        {
            velocitiyX = 0;
        }
        if (forwardPressed && runPressed && velocitiyZ > currentMaxVelocity)
        {
            velocitiyZ = currentMaxVelocity;
        }
        else if (forwardPressed && velocitiyZ > currentMaxVelocity)
        {
            velocitiyZ -= Time.deltaTime * decelaration;
            if (velocitiyZ > currentMaxVelocity && velocitiyZ > (currentMaxVelocity - maxRunVelocity))
            {
                velocitiyZ = currentMaxVelocity;
            }
        }
        else if (forwardPressed && velocitiyZ < currentMaxVelocity && velocitiyZ > (currentMaxVelocity - maxRunVelocity))
        {
            velocitiyZ = currentMaxVelocity;
        }
    }
    #endregion
    #region input/set current velocity  (update)
    void Update()
    {
        bool forwardPressed = im.forwardPressed;
        bool backwardsPressed = im.backwardsPressed;
        bool leftPressed = im.leftPressed;
        bool rightPressed = im.rightPressed;
        bool runPressed = im.runPressed;

        //set current
        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;    


        ChangeVelocity( forwardPressed,  backwardsPressed,  leftPressed,  rightPressed,  runPressed,  currentMaxVelocity);
        LockOrRessetVelocity(forwardPressed, backwardsPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);

        anim.SetFloat("Velocity z", velocitiyZ);
        anim.SetFloat("Velocity x", velocitiyX);
    }
    #endregion
}