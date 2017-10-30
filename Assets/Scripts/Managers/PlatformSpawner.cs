using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : Singleton<PlatformSpawner> {

    //Array of platforms used for generator
    public GameObject[] platforms;

    //Vector3 spawnPosition;
    //height at which 
    float bottom = -.5f;
    float top = 3.8f;

    [Range (-1.7f,5.4f)]
    float heightGeneratedAt = 0f;
    float previousHeight, nextHeight;
    int cap = 5;

    //offset used as buffer between player and spawner offscreen
    public Vector2 offset = new Vector2 (2f,0f);

    // Use this for initialization
    void Start ()
    {
        /*Assigns a temp variable vector3 to players position then adds
        an offset that then keeps ahead of the player to a certain extent 
        while generating platforms*/
        SpawnNewPlatformAt(Vector3.zero);
    }

    float RandFloat (float lower, float upper)
    {
        float randFloat = 0f;
        randFloat = Random.Range(lower, upper);
        return randFloat;
    }

    int RandInt(int lower, int upper)
    {
        int randInt = 0;
        randInt = Random.Range(lower, upper);
        return randInt;
    }

    public void SpawnNewPlatformAt(Vector3 pos)
    {
        previousHeight = nextHeight;

        // just for debugging
        // remove later
        if (cap < 0)
        {
            return;
        }
        cap--;
        float randFloat = RandFloat(bottom, top);
        pos.y = randFloat;
        GameObject tempPlatform = Instantiate(platforms[RandInt(0, platforms.Length - 1)], pos, Quaternion.identity);
    }
}
