using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HopBlock : MonoBehaviour
{
    [Header("Hopblock values")]
    public bool begin;
    public bool end;
    public Hops hops;
    [Header("script componants")]
    public bool trigger;
    public bool beginTrigger;
    public bool endTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!beginTrigger && !endTrigger)
            {
                if (trigger && hops.begin)
                {
                    hops.index++;
                    trigger = false;
                }
                else if (trigger && hops.end)
                {
                    hops.index--;
                    trigger = false;
                }
            }
            else if (beginTrigger)
            {
                hops.begin = true;                
            }
            else if (endTrigger)
            {
                hops.end = true;
            }
        }
    }
}