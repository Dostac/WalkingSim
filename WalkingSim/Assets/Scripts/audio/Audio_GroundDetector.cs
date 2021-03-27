using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_GroundDetector : MonoBehaviour
{
    [Header("Terrain Detection Componants")]
    public Transform playerTransform;
    public Terrain t;
    [Space(5)]
    public int posX;
    public int posZ;
    public float[] textureValues;
    [Header("Layers")]
    public LayerMask grass;
    public LayerMask concrete;
    public LayerMask tube;
    public LayerMask water;
    public LayerMask container;
    public LayerMask sandBag;
    [Space(10)]
    [Header("General Sounds")]
    public AudioSource jumpSound;
    public AudioSource ledgeMoveSound;
    public AudioSource rollSound;
    public AudioSource landingSound;
    public AudioSource climbingSound;
    public AudioSource inAirSound;
    public AudioSource vaultingSound;
    public AudioSource slideSound;
    public AudioSource walkingSound;
    public AudioSource runningSound;
    [Header("grass Sounds")]
    public AudioSource slideSound_Grass;
    public AudioSource walkingSound_Grass;
    public AudioSource runningSound_Grass;
    [Header("gravel Sounds")]
    public AudioSource slideSound_Gravel;
    public AudioSource walkingSound_Gravel;
    public AudioSource runningSound_Gravel;
    [Header("concrete Sounds")]
    public AudioSource slideSound_Concrete;
    public AudioSource walkingSound_Concrete;
    public AudioSource runningSound_Concrete;
    [Header("tube Sounds")]
    public AudioSource slideSound_Tube;
    public AudioSource walkingSound_Tube;
    public AudioSource runningSound_Tube;
    [Header("water Sounds")]
    public AudioSource walkingSound_Water;
    public AudioSource runningSound_Water;
    [Header("container Sounds")]
    public AudioSource slideSound_Container;
    public AudioSource walkingSound_Container;
    public AudioSource runningSound_Container;
    [Header("sandBag Sounds")]
    public AudioSource slideSound_Sandbag;
    public AudioSource walkingSound_Sandbag;
    public AudioSource runningSound_Sandbag;
    #region sounds
    public void RunningSound()
    {
        SoundReset();
        runningSound.Play();
    }
    public void WalkingSound()
    {
        SoundReset();
        walkingSound.Play();
    }
    public void JumpingSound()
    {
        SoundReset();
        jumpSound.Play();
    }
    public void SlidingSound()
    {
        SoundReset();
        slideSound.Play();
    }
    public void LandingSound()
    {
        SoundReset();
        landingSound.Play();
    }
    public void InAirSound()
    {
        SoundReset();
        inAirSound.Play();
    }
    public void RollSound()
    {
        SoundReset();
        rollSound.Play();
    }
    public void LedgeMoveSound()
    {
        SoundReset();
        ledgeMoveSound.Play();
    }
    public void VaultSound()
    {
        SoundReset();
        vaultingSound.Play();
    }
    public void ClimbSound()
    {
        SoundReset();
        climbingSound.Play();
    }
    public void SoundReset()
    {
        jumpSound.Stop();
        runningSound.Stop();
        walkingSound.Stop();
        ledgeMoveSound.Stop();
        rollSound.Stop();
        landingSound.Stop();
        inAirSound.Stop();
        slideSound.Stop();
        vaultingSound.Stop();
        climbingSound.Stop();
        SoundReset();
    }
    #endregion
    void Start()
    {
        t = Terrain.activeTerrain;
        playerTransform = gameObject.transform;
    }

    public void GetTerrainTexture()
    {
        ConvertPosition(playerTransform.position);
        CheckTexture();
    }

    void ConvertPosition(Vector3 playerPosition)
    {
        Vector3 terrainPosition = playerPosition - t.transform.position;

        Vector3 mapPosition = new Vector3
        (terrainPosition.x / t.terrainData.size.x, 0,
        terrainPosition.z / t.terrainData.size.z);

        float xCoord = mapPosition.x * t.terrainData.alphamapWidth;
        float zCoord = mapPosition.z * t.terrainData.alphamapHeight;

        posX = (int)xCoord;
        posZ = (int)zCoord;
    }

    void CheckTexture()
    {
        float[,,] aMap = t.terrainData.GetAlphamaps(posX, posZ, 1, 1);
        textureValues[0] = aMap[0, 0, 0];
        textureValues[1] = aMap[0, 0, 1];
        textureValues[2] = aMap[0, 0, 2];
        textureValues[3] = aMap[0, 0, 3];
    }

    public void Update()
    {
        GetTerrainTexture();
        RaycastHit FallDetection;
        Ray rayDir = new Ray(transform.position + (new Vector3(0f, 0.25f, 0f)), -transform.up);
        if (Physics.Raycast(rayDir, out FallDetection, 5f))
        {
            ///doe hier de detection van de terrain en die meuk
            if (FallDetection.transform.gameObject.layer == concrete)
            {
                slideSound = slideSound_Concrete;
                walkingSound = walkingSound_Concrete;
                runningSound = runningSound_Concrete;
            }
            else if (FallDetection.transform.gameObject.layer == container)
            {
                slideSound = slideSound_Container;
                walkingSound = walkingSound_Container;
                runningSound = runningSound_Container;
            }
            else if (FallDetection.transform.gameObject.layer == water)
            {
                walkingSound = walkingSound_Water;
                runningSound = runningSound_Water;
            }
            else if (FallDetection.transform.gameObject.layer == sandBag)
            {
                slideSound = slideSound_Sandbag;
                walkingSound = walkingSound_Sandbag;
                runningSound = runningSound_Sandbag;
            }
            else if (FallDetection.transform.gameObject.layer == tube)
            {
                slideSound = slideSound_Tube;
                walkingSound = walkingSound_Tube;
                runningSound = runningSound_Tube;
            }
        }
    }
}