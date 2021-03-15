using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WallCollision : MonoBehaviour
{
    #region public componants
    [Header("bools to check")]
    [Tooltip("bool that will be checked in an other script")]
    public bool medium;
    [Tooltip("bool that will be checked in an other script")]
    public bool vault;
    [Tooltip("bool that will be checked in an other script")]
    public bool ledge;
    [Tooltip("bool that will be checked in an other script")]
    public bool balancingBar;
    [Tooltip("bool that will be checked in an other script")]
    public bool balanceBegin;
    [Tooltip("bool that will be checked in an other script")]
    public bool balanceEnd;
    [Tooltip("bool that will be checked in an other script")]
    public bool laderbool;
    [Space(10)]
    [Tooltip("the destenation that the code wil set and the playermovement wil get")]
    public Transform destenation;
    #endregion
    #region private componants
    private MediumWall mediumwall = null;
    private VaultWall vaultwall = null;
    private Ledge legecol = null;
    private BalancingBar balancB = null;
    private Lader lader = null;
    #endregion
    #region collision
    public void OnTriggerEnter(Collider other)
    {
        vaultwall = other.gameObject.GetComponent<VaultWall>();
        legecol = other.gameObject.GetComponent<Ledge>();
        lader = other.gameObject.GetComponent<Lader>();
        mediumwall = other.gameObject.GetComponent<MediumWall>();
        balancB = other.gameObject.GetComponent<BalancingBar>();
        if (vaultwall != null)
        {
            vault = true;
            destenation = other.gameObject.GetComponent<VaultWall>().destenation;
        }
        else if (mediumwall != null)
        {
            medium = true;
            destenation = other.gameObject.GetComponent<MediumWall>().destenation;
        }
        else if (legecol != null)
        {
            ledge = true;
            destenation = other.gameObject.GetComponent<Ledge>().climbPoint;
        }
        else if (balancB != null)
        {
            balancingBar = true;
            destenation = other.gameObject.GetComponent<BalancingBar>().destenation;
        }
        else if (lader != null)
        {
            laderbool = true;            
        }
    }
    public void OnTriggerExit(Collider other)
    {
        mediumwall = other.gameObject.GetComponent<MediumWall>();
        vaultwall = other.gameObject.GetComponent<VaultWall>();
        legecol = other.gameObject.GetComponent<Ledge>();
        balancB = other.gameObject.GetComponent<BalancingBar>();
        lader = other.gameObject.GetComponent<Lader>();
        if (legecol != null)
        {
            ledge = false;
        }
        else if (mediumwall != null)
        {
            medium = false;
        }
        else   if (vaultwall != null)
        {
            vault = false;
        }
        else if (balancB != null)
        {
            balancingBar = false;
        }
        else if(lader != null)
        {
            laderbool = false;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        legecol = other.gameObject.GetComponent<Ledge>();
        if (legecol != null)
        {
            ledge = true;
        }
    }
    #endregion
}