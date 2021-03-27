using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public int color;

    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    public Material mat5;
    public Material mat6;
    public Material mat7;
    public Material mat8;

    public GameObject[] renderers;

    public void UpdateMaterial()
    {
        if (color == 0)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                MaterialIdentifier id = renderers[i].GetComponent<MaterialIdentifier>();
                if (id.isDoor == true)
                {
                    renderers[i].GetComponent<MeshRenderer>().material = mat2;
                }
                else
                {
                    renderers[i].GetComponent<MeshRenderer>().material = mat1;
                }
            }
        }

        if (color == 1)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                MaterialIdentifier id = renderers[i].GetComponent<MaterialIdentifier>();
                if (id.isDoor == true)
                {
                    renderers[i].GetComponent<MeshRenderer>().material = mat4;
                }
                else
                {
                    renderers[i].GetComponent<MeshRenderer>().material = mat3;
                }
            }
        }

        if (color == 2)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                MaterialIdentifier id = renderers[i].GetComponent<MaterialIdentifier>();
                if (id.isDoor == true)
                {
                    renderers[i].GetComponent<MeshRenderer>().material = mat6;
                }
                else
                {
                    renderers[i].GetComponent<MeshRenderer>().material = mat5;
                }
            }
        }

        if (color == 3)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                MaterialIdentifier id = renderers[i].GetComponent<MaterialIdentifier>();
                if (id.isDoor == true)
                {
                    renderers[i].GetComponent<MeshRenderer>().material = mat8;
                }
                else
                {
                    renderers[i].GetComponent<MeshRenderer>().material = mat7;
                }
            }
        }
    }
}
