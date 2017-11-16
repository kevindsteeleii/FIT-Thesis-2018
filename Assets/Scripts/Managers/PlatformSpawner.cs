using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : Singleton<PlatformSpawner>
{
    //Array of platforms used as pool for platform spawner
    public GameObject[] platforms;
    //List that populates with all the platforms made by spawner to allow access by external components
    public List<GameObject> spawnedPlatforms;
    //Height ranges to spawn at
    float bottom = -.5f;
    float top = 3.8f;
    bool generated = false;

    /// <summary>
    /// Event broadcast from PlatformSpawner upon spawning platforms
    /// </summary>
    public event Action<Vector3> PlatformsHaveSpawned;

    /// <summary>
    /// Event that broadcasts to the enemySpawner to pass to the spawned enemyies' patrol component certain data.
    /// </summary>
    public event Action <Platforms> PlatformPass;

    // variables used to "save" previous and next heights of the 
    float previousHeight, nextHeight;
    int cap = 5;

    //offset used as buffer between player and spawner offscreen
    public Vector2 offset = new Vector2(2f, 0f);

    // Use this for initialization
    void Start()
    {
        spawnedPlatforms = new List<GameObject>();

        /*Assigns a temp variable vector3 to players position then adds
        an offset that then keeps ahead of the player to a certain extent 
        while generating platforms*/
        SpawnNewPlatformAt(Vector3.zero);
        GameManager.instance.Restarting += ReSpawn;
        GUIManager.instance.Restarted += ReSpawn;
    }

    private void Update()
    {
        if (PlatformsHaveSpawned != null && PlatformPass != null  && !generated)
        {
            StartCoroutine("SendPositions");
            generated = true;
        }
    }

    /// <summary>
    /// Respawns the level layout after each player generated restart
    /// </summary>
    void ReSpawn()
    {
        Debug.Log(" Platforms are supposed to Respawn Here!!");
        cap = 5;
        generated = false;
        //SpawnNewPlatformAt(respawnPos);
        SpawnNewPlatformAt(Vector3.zero);
    }

    /// <summary>
    /// Coroutine used to transmit the vector3 center position of each generated platform in turn
    /// </summary>
    /// <returns></returns>
    System.Collections.IEnumerator SendPositions()
    {
        yield return null;

        foreach (GameObject item in spawnedPlatforms)
        {
            Platforms temp = item.GetComponentInChildren<Platforms>();
            PlatformPass(temp);
            Vector3 pos = temp.GetCenter();
            PlatformsHaveSpawned(pos);
        }
    }

    /// <summary>
    /// Returns random float
    /// </summary>
    /// <param name="lower"></param>
    /// <param name="upper"></param>
    /// <returns></returns>
    float RandFloat(float lower, float upper)
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
    int RandInt(int lower, int upper)
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

        //prevents same height instantiations of platforms spawned, loops back if same goes at least once before checking loop condition
        do
        {
            float randFloat = RandFloat(bottom, top);
            nextHeight = randFloat;
            pos.y = randFloat;
        }

        while (nextHeight == previousHeight);

        //prevents too much space between next generated platform to allow a jumpable height
        if (Mathf.Abs(previousHeight - nextHeight) > 3.6)
        {
            float dif = Mathf.Abs(previousHeight - nextHeight);
            nextHeight -= 1;
        }
        //assigns previous to next height as to set up comparison for next go around in the do-while loop
        previousHeight = nextHeight;

        GameObject tempPlatform = Instantiate(platforms[RandInt(0, platforms.Length - 1)], pos, Quaternion.identity);
        tempPlatform.transform.SetParent(this.gameObject.transform);
        //adds spawned platform to the list used for enemy spawning
        spawnedPlatforms.Add(tempPlatform);
        Vector3 loc = tempPlatform.transform.position;
    }

}
