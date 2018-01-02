using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Class used to handle enemy spawning and other such logic, mostly temporary measures at this point < Kev Note
/// </summary>
public class EnemySpawner : Singleton<EnemySpawner>
{
    #region Variables
    public GameObject enemy;

    [Tooltip("This float is the offset from the center of the platform vertically.")]
    [Range(0, 2)]
    public float yOffset;

    [SerializeField]
    Platforms TempPlatforms;
    #endregion

    // Use this for initialization
    void Start()
    {
        PlatformSpawner.instance.On_PlatformsHaveSpawned_Sent += SpawnEnemies;
        PlatformSpawner.instance.On_PlatformPass_Sent += SetTempPlatform;
    }

    //function that subscribes to the platformSpawner's event that passes list of vector3s of the platforms created
    public void SpawnEnemies(Vector3 platPos)
    {
        //adds offset to the new position
        platPos.y += yOffset;
        int randInt = UnityEngine.Random.Range(0, 100);

        //if even number then appear
        if (randInt % 2 == 0)
        {
            //Debug.Log(platPos + " is the location of a platform \n");
            GameObject tempObj = Instantiate(enemy, platPos, Quaternion.identity);
            tempObj.GetComponentInChildren<Enemy>().SetPlatform(TempPlatforms);
            tempObj.transform.SetParent(this.gameObject.transform);
        }

        return;
    }

    /// <summary>
    /// Sets the platform of each enemy in turn
    /// </summary>
    /// <param name="platforms"></param>
    public void SetTempPlatform(Platforms platforms)
    {
        TempPlatforms = platforms;
    }

}

