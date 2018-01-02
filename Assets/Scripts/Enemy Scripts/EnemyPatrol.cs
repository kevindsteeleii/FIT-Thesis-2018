using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles the enemy patrol behavior
/// </summary>
public class EnemyPatrol : MonoBehaviour
{
    // the platform the enemy is perched upon
    [SerializeField] Platforms enemyPost;

    //xOffset from edge of platform to be patrolled to keep enemy from falling off
    [Range(0f, 1f)]
    public float xOffset = 0.05f;

    //speed relative to Time.time of the patrolling/pacing enemy
    [Range(0.01f, 1f)]
    public float speed = 0.02f;

    //bool that determines whether or not the enemy is patrolling
    bool patrolling = false;

    //location of the center of movement
    Vector3 centerVec;

    /// <summary>
    /// Sets up the platform's info that the 
    /// </summary>
    /// <param name="platform"></param>
    void SetUpPatrolObject (Platforms platform)
    {
        enemyPost = platform;
    }

    // Use this for initialization
    void Start()
    {
        //assigns the beginning and end of the collider of the platform the enemy will be patrolling upon
        //startPatrolling = enemyPost.startPatrol;
        //endPatrolling = enemyPost.endPatrol;
        this.gameObject.GetComponent<Enemy>().SendBehavior += PatrolBehave;
        this.gameObject.GetComponent<Enemy>().EnPlatformIs += PlatformGet;
        this.gameObject.GetComponent<Enemy>().PlatformMoved += GetCentered;
    }

    private void GetCentered(Vector3 tempVec)
    {
        centerVec = tempVec;
    }

    void PlatformGet(Platforms obj)
    {
        enemyPost = obj;
    }

    /// <summary>
    /// Function that is subscriber of the Enemy Event SendBehavior. This should be the template for future behavior components.
    /// </summary>
    /// <param name="obj"></param>
    public void PatrolBehave(EnemyBehavior obj)
    {
        if (obj != EnemyBehavior.Patrolling)
        {
            patrolling = false;
        }

        else
        {
            patrolling = true;
        }
    }

    void Update()
    {
        Vector3 pos = centerVec;
        float distance = enemyPost.delta - xOffset;

        if (!patrolling)
        {
            return;
        }

        else
        {
            //oscillates between either extreme with an offset with specified timing
            pos.x += distance * Mathf.Sin(Time.time * speed);
            transform.position = pos;
        }
    }
}
