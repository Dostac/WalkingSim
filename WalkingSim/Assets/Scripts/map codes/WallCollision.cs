using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WallCollision : MonoBehaviour
{
    [Header("bools to check")]
    public bool large;
    public bool medium;
    public bool vault;
    public bool lege;
    [Header("Pivot Transforms")]
    public Transform beginPivot;
    public Transform endPivot;

    LargeWall largewall = null;
    MediumWall mediumwall = null;
    VaultWall vaultwall = null;
    Lege legecol = null;

    public void OnTriggerEnter(Collider other)
    {
        largewall = other.gameObject.GetComponent<LargeWall>();
        if (largewall != null)
        {
            large = true;
            beginPivot = other.gameObject.GetComponent<LargeWall>().beginPivot;
            endPivot=other.gameObject.GetComponent<LargeWall>().endPivot;
        }
        mediumwall = other.gameObject.GetComponent<MediumWall>();
        if (mediumwall != null)
        {
            medium = true;
            beginPivot = other.gameObject.GetComponent<MediumWall>().beginPivot;
            endPivot = other.gameObject.GetComponent<MediumWall>().endPivot;
        }
        vaultwall = other.gameObject.GetComponent<VaultWall>();
        if (vaultwall != null)
        {
            vault = true;
            beginPivot = other.gameObject.GetComponent<VaultWall>().beginPivot;
            endPivot = other.gameObject.GetComponent<VaultWall>().endPivot;
        }
        legecol = other.gameObject.GetComponent<Lege>();
        if (legecol != null)
        {
            lege = true;
            beginPivot = other.gameObject.GetComponent<Lege>().beginPivot;
            endPivot = other.gameObject.GetComponent<Lege>().endPivot;
        }
    }
    private void OnTriggerExit(Collider other)
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
        legecol = other.gameObject.GetComponent<Lege>();
        if (legecol != null)
        {
            lege = false;

        }
    }
}