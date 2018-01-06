using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : Singleton<PlatformSpawner>
{
    //Array of platforms used as pool for platform spawner
    [SerializeField]
    GameObject[] platforms;
    //List that populates with all the platforms made by spawner to allow access by external components
    public List<GameObject> spawnedPlatforms;

    //Height ranges to spawn at
    float bottom = -.5f;
    float top = 3f;
    bool generated = false;

    /// <summary>
    /// Event broadcast from PlatformSpawner upon spawning platforms
    /// </summary>
    public event Action<Vector3> On_PlatformsHaveSpawned_Sent;

    /// <summary>
    /// Event that broadcasts to the enemySpawner to pass to the spawned enemyies' patrol component certain data.
    /// </summary>
    public event Action<Platforms> On_PlatformPass_Sent;

    // variables used to "save" previous and next heights of the 
    float previousHeight, nextHeight;
    //variable used for the test number of random platforms generated and the number of each platform loaded for the object pooled version of spawner
    int cap = 5;

    //array that holds the original "addresses" of the platforms generated
    List<Vector3> platLocations;

    /// <summary>
    ///game object used to mark halfway point or rather check point to trigger last platform skipping to the front 
    /// </summary>
    public GameObject continuePoint;
    /// <summary>
    /// Transform that the continuePoint follows AKA the player character
    /// </summary>
    public Transform pointer;

    [Tooltip("Float that determines the amount of gap between teleporting platforms")]
    [Range(0.01f, 1f)]
    public float horizontalBuffer;

    // Use this for initialization
    void Start()
    {
        spawnedPlatforms = new List<GameObject>();

        /*Assigns a temp variable vector3 to players position then adds
        an offset that then keeps ahead of the player to a certain extent 
        while generating platforms*/
        SpawnNewPlatformAt(Vector3.zero);
        GameManager.instance.On_RestartState_Sent += On_ReStartState_Caught;

        continuePoint.transform.SetParent(pointer);
    }


    private void Update()
    {
        if (On_PlatformsHaveSpawned_Sent != null && On_PlatformPass_Sent != null && !generated)
        {
            StartCoroutine("SendPositions");
        }
    }

    private void FixedUpdate()
    {
        PlatformTeleport(continuePoint.transform.position);
    }

    /// <summary>
    /// Returns position of the object entered
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    protected virtual Vector3 GetPosition(GameObject obj)
    {
        return obj.transform.position;
    }

    /// <summary>
    /// Adds gameObject world location one object at a time.
    /// </summary>
    /// <param name="temp"></param>
    protected virtual void SavePlatformAddresses(GameObject temp)
    {
        platLocations.Add(temp.transform.position);
    }

    /// <summary>
    /// On reset, sets all platforms back to original world locations.
    /// </summary>
    protected virtual void ReloadPlatformAddresses()
    {
        for (int i = 0; i < spawnedPlatforms.Count; i++)
        {
            spawnedPlatforms[i].transform.position = platLocations[i];
        }
    }


    /// <summary>
    ///  Compares all platforms to see which is the farthest to the right and returns its corresponding array address.
    /// </summary>
    /// <returns></returns>
    protected virtual int GetFarPlatform()
    {
        int address = 0;
        for (int i = 0; i < spawnedPlatforms.Count; i++)
        {
            if (spawnedPlatforms[address].transform.position.x < spawnedPlatforms[i].transform.position.x)
            {
                address = i;
            }
        }
        return address;
    }

    /// <summary>
    ///  Compares all platforms to see which is the closest to the left and returns its corresponding array address.
    /// </summary>
    /// <returns></returns>
    protected virtual int GetClosePlatform()
    {
        int address = 0;
        for (int i = 0; i < spawnedPlatforms.Count; i++)
        {
            if (spawnedPlatforms[address].transform.position.x > spawnedPlatforms[i].transform.position.x)
            {
                address = i;
            }
        }
        return address;
    }

    /// <summary>
    /// Method that checks to see if the continue checkpoint has passed a 
    /// platform and proceeds to teleport it to the front upon a positive outcome
    /// </summary>
    protected virtual void PlatformTeleport(Vector3 checkPoint)
    {
        foreach (GameObject obj in spawnedPlatforms)
        {
            if (obj.transform.position.x < continuePoint.transform.position.x)
            {
                obj.SetActive(false);
                float length = horizontalBuffer + spawnedPlatforms[GetFarPlatform()].transform.position.x + spawnedPlatforms[GetFarPlatform()].GetComponentInChildren<Collider>().bounds.size.x;
                obj.transform.position = new Vector3(length, obj.transform.position.y, obj.transform.position.z);
                obj.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Respawns the level layout after each player generated restart
    /// </summary>
    protected virtual void On_ReStartState_Caught()
    {
        Debug.Log(" Platforms are supposed to Respawn Here!!");
        //SpawnNewPlatformAt(Vector3.zero);
        ReloadPlatformAddresses();
    }

    /// <summary>
    /// Coroutine used to transmit the vector3 center position of each generated platform in turn
    /// </summary>
    /// <returns></returns>
    IEnumerator SendPositions()
    {
        yield return null;

        foreach (GameObject item in spawnedPlatforms)
        {
            Platforms temp = item.GetComponentInChildren<Platforms>();
            On_PlatformPass_Sent(temp);
            Vector3 pos = temp.GetCenter();
            On_PlatformsHaveSpawned_Sent(pos);
        }
        generated = true;
    }

    /// <summary>
    /// Returns random float
    /// </summary>
    /// <param name="lower"></param>
    /// <param name="upper"></param>
    /// <returns></returns>
    protected virtual float RandFloat(float lower, float upper)
    {
        float randFloat = 0f;
        randFloat = UnityEngine.Random.Range(lower, upper);
        return randFloat;
    }

    /// <summary>
    /// returns random int
    /// </summary>
    /// <param name="lower"></param>
    /// <param name="upper"></param>
    /// <returns></returns>
    protected virtual int RandInt(int lower, int upper)
    {
        int randInt = 0;
        randInt = (int)RandFloat(lower, upper);
        return randInt;
    }

    /// <summary>
    /// Spawns new platforms at Vector3 specified
    /// </summary>
    /// <param name="pos"></param>
    public void SpawnNewPlatformAt(Vector3 pos)
    {
        // just for debugging
        // remove later, the cap part
        if (cap < 0)
        {
            return;
        }

        cap--;

        #region Random non-repeating height generating code to be encapsulated in own individual method later on...
        //prevents same height instantiations of platforms spawned, loops back if same goes at least once before checking loop condition
        do
        {
            float randFloat = RandFloat(bottom, top);
            nextHeight = randFloat;
            pos.y = randFloat;
        }

        while (nextHeight == previousHeight);

        //prevents too much space between next generated platform to allow a jumpable height
        if (Mathf.Abs(previousHeight - nextHeight) > 3.0)
        {
            float dif = Mathf.Abs(previousHeight - nextHeight);
            nextHeight -= 1;
        }
        //assigns previous to next height as to set up comparison for next go around in the do-while loop
        previousHeight = nextHeight;
        #endregion

        GameObject tempPlatform = Instantiate(platforms[RandInt(0, platforms.Length - 1)], pos, Quaternion.identity);
        tempPlatform.transform.SetParent(this.gameObject.transform);
        //adds spawned platform to the list used for enemy spawning
        spawnedPlatforms.Add(tempPlatform);
        SavePlatformAddresses(tempPlatform);
    }

}
