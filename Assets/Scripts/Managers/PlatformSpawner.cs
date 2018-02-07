using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class that handles the logic and events of the individual platforms spawned
/// </summary>
public class PlatformSpawner : Singleton<PlatformSpawner>
{
    //Array of platforms used as pool for platform spawner
    [SerializeField]
    private GameObject[] platforms; //used during the initial platform load
    //List that populates with all the platforms made by spawner to allow access by external components
    public List<GameObject> spawnedPlatforms;

    [Tooltip("Vertical Offset of the enemy spawned on a platform.")]
    [Range(0f, 5f)]
    public float yOffset = 2f;

    //Height ranges to spawn at
    float bottom = -.5f;
    float top = 2.8f;

    // variables used to "save" previous and next heights of the 
    float previousHeight, nextHeight;

    [Range(2f, 20f)]
    [Tooltip("This number is the number of generated platforms upon game start.")]
    public int platformNumber = 3;

    //array that holds the original center "addresses" of the platforms generated
    public List<Vector3> platLocations;

    //List that holds the proper location of the platforms as opposed to their center address used for the enemy spawner
    public List<Vector3> trueLocations;

    /// <summary>
    /// Broadcasts corresponding collection address for 
    /// </summary>
    public event Action <int> On_Teleport_Sent;

    /// <summary>
    /// Broadcast the list of platform locations from spawn until game over/ restart
    /// </summary>
    public event Action<List<Vector3>> On_PlatLocations_Sent;

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
        MakeSomePlatforms(Vector3.zero);
        continuePoint.transform.SetParent(pointer);
        On_Teleport_Sent += EnemySpawner.instance.On_Teleport_Received;
        GameManager.instance.onRestartState += On_ReStartState_Caught;
        PlatformRemoverControl.instance.On_PlatformRemoverPass_Sent += PlatformTeleport;
    }

    private void Update()
    {
        //sends the vector3 locations of the platforms in there exists a subscriber
        if (On_PlatLocations_Sent != null)
        {
            On_PlatLocations_Sent(platLocations);
        }
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
        //Debug.Log("Platform position is " + temp.transform.position);
        trueLocations.Add(temp.transform.position);
        Vector3 centerLoc = temp.gameObject.GetComponentInChildren<Collider>().bounds.center;
        centerLoc.y += yOffset;
        platLocations.Add(centerLoc);
    }

    /// <summary>
    /// On reset, sets all platforms back to original world locations.
    /// </summary>
    protected virtual void ReloadPlatformAddresses()
    {
        for (int i = 0; i < spawnedPlatforms.Count; i++)
        {
            spawnedPlatforms[i].transform.position = trueLocations[i];
            Vector3 pos = spawnedPlatforms[i].GetComponentInChildren<Collider>().bounds.center;
            //temporary measure as of now
            pos.y += yOffset;
            platLocations[i] = pos;
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
        for (int i = 0; i < spawnedPlatforms.Count; i++)
        {
            GameObject obj = spawnedPlatforms[i];
            if (obj.transform.position.x < continuePoint.transform.position.x)
            {
                obj.SetActive(false);
                float length = horizontalBuffer + spawnedPlatforms[GetFarPlatform()].transform.position.x + spawnedPlatforms[GetFarPlatform()].GetComponentInChildren<Collider>().bounds.size.x;
                obj.transform.position = new Vector3(length, obj.transform.position.y, obj.transform.position.z);
                Vector3 centerLoc = obj.GetComponentInChildren<Collider>().bounds.center;
                platLocations[i] = centerLoc;
                On_Teleport_Sent(i);
                obj.SetActive(true);
            }
            else
            {
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
    /// A recursive platform generator that only needs the starting position and the upper limit of platforms being generated
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="limit"></param>
    protected virtual void MakeSomePlatforms(Vector3 startPos)
    {
        if (platformNumber <= 0)
            return;
        else
        {
            #region Random non-repeating height generating code to be encapsulated in own individual method later on...
            //prevents same height instantiations of platforms spawned, loops back if same goes at least once before checking loop condition
            do
            {
                float randFloat = RandFloat(bottom, top);
                nextHeight = randFloat;
                startPos.y = randFloat;
            }

            while (nextHeight == previousHeight);

            //prevents too much space between next generated platform to allow a jumpable height
            if (Mathf.Abs(previousHeight - nextHeight) > 2.75f)
            {
                float dif = Mathf.Abs(previousHeight - nextHeight);
                nextHeight -= 1;
            }
            //assigns previous to next height as to set up comparison for next go around in the do-while loop
            previousHeight = nextHeight;
            #endregion

            GameObject tempPlatform = Instantiate(platforms[RandInt(0, platforms.Length - 1)], startPos, Quaternion.identity);
            platformNumber--;
            tempPlatform.transform.SetParent(this.gameObject.transform);
            //get the farthest edge's location to be used for the random horizontal space between gap 
            Vector3 tempPos = tempPlatform.gameObject.GetComponentInChildren<Collider>().bounds.max;
            float gap = RandFloat(0.5f, .75f) + tempPos.x - startPos.x; //subtract tempPos from the startPos to use the difference as the distance that the gap covers
            startPos.x += gap;
            spawnedPlatforms.Add(tempPlatform); //add the platform to list of objects that are spawning
            SavePlatformAddresses(tempPlatform); //this adds the vector3 addresses of the generated platforms 
            MakeSomePlatforms(startPos);
        }
    }
}
