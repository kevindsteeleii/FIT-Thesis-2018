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

    [SerializeField]
    List<GameObject> enemyTroops = new List<GameObject>();

    //list that is used to check and record whether a corresponding enemy in the address was spawned or not
    public List<bool> didGenerate = new List<bool>();

    //variable used to store the respective locations of the platforms
    List<Vector3> platLocations = new List<Vector3>();
    
    int enemyCount = 0;

    #endregion

    // Use this for initialization
    void Start()
    {
        //sends the list of the vector3 locations of the platforms
        PlatformSpawner.instance.On_PlatLocations_Sent += On_PlatLocations_Received;
        GameManager.instance.onRestartState += On_ReStartState_Received;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < platLocations.Count-1; i++)
        {
            enemyTroops[i].transform.position = platLocations[i];
        }
    }

    /// <summary>
    /// Function that subscribes to the GameManager and "resets" the positions of the enemies upon restart
    /// </summary>
    private void On_ReStartState_Received()
    {
        for (int i = 0; i < platLocations.Count; i++)
        {
            if (didGenerate[i])
            {
                enemyTroops[i].transform.position = platLocations[i];
            }
        }
    }

    /// <summary>
    /// Recursive enemy spawner
    /// </summary>
    /// <param name="startPos"></param>
    void MakeSomeEnemies(Vector3 startPos)
    {
        if (enemyCount >= platLocations.Count)
            return;

        else
        {
            GameObject tempObj = Instantiate(enemy);
            enemy.transform.SetParent(gameObject.transform);
            enemy.transform.position = startPos;
            enemyTroops.Add(tempObj);

            enemyCount++;
            MakeSomeEnemies(platLocations[enemyCount]);
        }
    }

    //function that returns a random int
    int RandomInt(int a, int b)
    {
        int c = (int)UnityEngine.Random.Range(a, b);
        return c;
    }

    /// <summary>
    /// This Function recursively produces enemies and creates an array that keeps account of whether or not the enemy was spawned randomly at the start
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="generate"></param>
    void MakeSomeEnemiesRandom(Vector3 pos)
    {
        int randInt = RandomInt(0, 12);
        bool didSpawn = randInt % 2 == 0 ? true : false;
        enemyCount++;

        if ( 0 <= didGenerate.Count && didGenerate.Count<= platLocations.Count-1 )
            didGenerate.Add(didSpawn);

        if (enemyCount >= platLocations.Count)
            return;

        else
        {
            GameObject tempObj = Instantiate(enemy);
            tempObj.transform.SetParent(this.gameObject.transform);
            tempObj.transform.position = pos;
            enemyTroops.Add(tempObj);
            tempObj.SetActive(didSpawn);
            MakeSomeEnemiesRandom(platLocations[enemyCount]);
        }
    }

    /// <summary>
    /// This function exists to subscribe to the events On_PlatLocations_Sent
    /// </summary>
    /// <param name="platLocList"></param>
    void On_PlatLocations_Received(List<Vector3> platLocList)
    {
        platLocations = platLocList;
        //MakeSomeEnemies(platLocations[0]);
        MakeSomeEnemiesRandom(platLocations[0]);
    }
} //end of class


