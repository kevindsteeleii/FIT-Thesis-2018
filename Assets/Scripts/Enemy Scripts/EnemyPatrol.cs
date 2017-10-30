using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Platforms))]
public class EnemyPatrol : MonoBehaviour
{
    // the platform the enemy is perched upon
    [SerializeField] Platforms enemyPost;

    //xOffset from edge of platform to be patrolled to keep enemy from falling off
    [Range(0f, 1f)]
    public float xOffset = 0.05f;

    //yOffset from the center of the platform to adjust height placement
    [Range(0f, 1f)]
    public float yOffset = 0.05f;

    //speed relative to Time.time of the patrolling/pacing enemy
    [Range(0.01f, 1f)]
    public float speed = 0.02f;

    //used to effect velocity of oscillation of patrolling enemy
    Rigidbody rb;

    //vectors used to get the beginning and end of the platform to be patrolled
    //Vector3 startPatrolling, endPatrolling;

    //sets default type of movement pattern here more to be added onto this later
    //public MovementType enemyMoves = MovementType.Horizontal;

    // Use this for initialization
    void Start()
    {
        //assigns the beginning and end of the collider of the platform the enemy will be patrolling upon
        //startPatrolling = enemyPost.startPatrol;
        //endPatrolling = enemyPost.endPatrol;
    }

    void Update()
    {
        Vector3 pos = enemyPost.transform.position;
        pos.y = enemyPost.transform.position.y + yOffset;

        float distance = enemyPost.delta - xOffset;

        //oscillates between either extreme with an offset with specified timing
        pos.x += distance * Mathf.Sin(Time.time * speed);
        transform.position = pos;
    }
}
