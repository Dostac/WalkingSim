using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResetPointScript : MonoBehaviour
{
    //public
    public Vector3 spawnPos;
    public bool setActieve, resetPos;
    //private
    private float posX, posY, posZ;
    private GameObject player;

    public void OnCheckPoint()
    {
        SetPosition();
        SavePos();
        GetPos();
        Respawn();
    }
    private void Start()
    {
        if (resetPos)
        {
            ResetPos();
        }
        if (setActieve)
        {

            player = GameObject.FindGameObjectWithTag("Player");
            posX = PlayerPrefs.GetFloat("posX");
            posY = PlayerPrefs.GetFloat("posY");
            posZ = PlayerPrefs.GetFloat("posZ");

            spawnPos = new Vector3(posX, posY, posZ);
            Respawn();
        }
    }
    public void SetPosition()
    {
        spawnPos = player.transform.position;

        posX = spawnPos.x;
        posY = spawnPos.y;
        posZ = spawnPos.z;
    }
    public void SavePos()
    {
        PlayerPrefs.SetFloat("posX", posX);
        PlayerPrefs.SetFloat("posY", posY);
        PlayerPrefs.SetFloat("posZ", posZ);
    }
   public void GetPos()
    {
       posX = PlayerPrefs.GetFloat("posX");
       posY = PlayerPrefs.GetFloat("posY");
       posZ = PlayerPrefs.GetFloat("posZ");
    }
    public void Respawn()
    {
        spawnPos = new Vector3(posX, posY, posZ);
        player.transform.position = spawnPos;
    }

    public void OnApplicationQuit()
    {
        SavePos();
    }
    public void ResetPos()
    {
        PlayerPrefs.SetFloat("posX", -9.927f);
        PlayerPrefs.SetFloat("posY", 0.196f);
        PlayerPrefs.SetFloat("posZ", 3.45f);
        spawnPos = new Vector3(posX, posY, posZ);
    }
} 