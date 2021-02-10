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
        }
        mediumwall = other.gameObject.GetComponent<MediumWall>();
        if (mediumwall != null)
        {
            medium = true;
        }
        vaultwall = other.gameObject.GetComponent<VaultWall>();
        if (vaultwall != null)
        {
            vault = true;
        }
        legecol = other.gameObject.GetComponent<Lege>();
        if (legecol != null)
        {
            lege = true;
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