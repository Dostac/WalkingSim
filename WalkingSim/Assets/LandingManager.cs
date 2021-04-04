using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingManager : MonoBehaviour
{
    public PlayerMovement pm;
    public void Roll()//called by animation
    {
        pm.landed = true;
        pm.rol = true;
        Invoke("ResetLanding", pm.landTimeRolling);
    }
    public void Landing()//called by animation
    {
        pm.landed = true;
        Invoke("ResetLanding", pm.landTimeLanding);
    }
    public void ResetLanding()
    {
        pm.landed = false;
        pm.rol = false;
    }
}
