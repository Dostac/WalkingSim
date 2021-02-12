using UnityEngine;
public class ResetPointScript : MonoBehaviour
{
    //public
    public Vector3 spawnPos;
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
    private void Awake()
    {
        player= GameObject.FindGameObjectWithTag("Player");

        posX = PlayerPrefs.GetFloat("posX");
        posY = PlayerPrefs.GetFloat("posY");
        posZ = PlayerPrefs.GetFloat("posZ");

        spawnPos = new Vector3(posX, posY, posZ);
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
        print("he ded");
    }

    void OnApplicationQuit()
    {
        SavePos();
    }
} 