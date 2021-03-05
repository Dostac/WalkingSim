using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WallCollision : MonoBehaviour
{
    [Header("bools to check")]
    public bool large;
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

    LargeWall largewall = null;
    MediumWall mediumwall = null;
    VaultWall vaultwall = null;
    Ledge legecol = null;
    BalancingBar balancB = null;
    Hops hops=null;
    Lader lader = null;

    public void OnTriggerEnter(Collider other)
    {
        largewall = other.gameObject.GetComponent<LargeWall>();
        if (largewall != null)
        {
            large = true;
            destenation = other.gameObject.GetComponent<LargeWall>().destenation;
        }
        vaultwall = other.gameObject.GetComponent<VaultWall>();
        if (vaultwall != null)
        {
            vault = true;
            destenation = other.gameObject.GetComponent<VaultWall>().destenation;
        }
        mediumwall = other.gameObject.GetComponent<MediumWall>();
        if (mediumwall != null)
        {
            medium = true;
            destenation = other.gameObject.GetComponent<MediumWall>().destenation;
        }
        legecol = other.gameObject.GetComponent<Ledge>();
        if (legecol != null)
        {
            ledge = true;
            destenation = other.gameObject.GetComponent<Ledge>().climbPoint;
        }
        balancB = other.gameObject.GetComponent<BalancingBar>();
        if (balancB != null)
        {
            balancingBar = true;
            destenation = other.gameObject.GetComponent<BalancingBar>().destenation;
        }
        lader = other.gameObject.GetComponent<Lader>();
        if (lader != null)
        {
            laderbool = true;            
        }
        hops = other.gameObject.GetComponent<Hops>();
        if (hops != null)
        {
            hop = true;
            destenation = other.gameObject.GetComponent<Hops>().destenation;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        largewall = other.gameObject.GetComponent<LargeWall>();
        if (largewall != null)
        {
            large = false;
        }
        mediumwall = other.gameObject.GetComponent<MediumWall>();
        if (mediumwall != null)
        {
            medium = false;
        }
        vaultwall = other.gameObject.GetComponent<VaultWall>();
        if (vaultwall != null)
        {
            vault = false;
        }
        legecol = other.gameObject.GetComponent<Ledge>();
        if (legecol != null)
        {
            ledge = false;
        }
        balancB = other.gameObject.GetComponent<BalancingBar>();
        if (balancB != null)
        {
            balancingBar = false;
        }
        lader = other.gameObject.GetComponent<Lader>();
        if (lader != null)
        {
            laderbool = false;
        }
        hops = other.gameObject.GetComponent<Hops>();
        if (hops != null)
        {
            hop = false;
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