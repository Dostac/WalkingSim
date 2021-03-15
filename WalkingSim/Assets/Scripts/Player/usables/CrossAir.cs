using UnityEngine;
public class CrossAir : MonoBehaviour
{
    private GameObject player;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Update()
    {
        transform.LookAt(player.transform);
    }
}