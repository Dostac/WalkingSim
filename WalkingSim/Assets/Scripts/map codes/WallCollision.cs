using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WallCollision : MonoBehaviour
{
    [Header("bools to check")]
    public bool medium;
    public bool vault;
    public bool ledge;
    public bool balancingBar;
    public bool balanceBegin;
    public bool balanceEnd;
    public bool laderbool;
    public bool hop;
    public bool isHopping;
    [Header("Pivot Transforms")]
    public Transform destenation;

    MediumWall mediumwall = null;
    VaultWall vaultwall = null;
    Ledge legecol = null;
    BalancingBar balancB = null;
    Lader lader = null;

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
}