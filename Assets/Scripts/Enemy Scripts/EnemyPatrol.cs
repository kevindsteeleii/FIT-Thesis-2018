using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyPatrol : MonoBehaviour
{
    // the platform the enemy is perched upon
    public Platforms enemyPost;

    //xOffset from edge of platform to be patrolled to keep enemy from falling off
    [Range(0f, 1f)]
    public float xOffset = 0;

    //speed relative to Time.time of the patrolling/pacing enemy
    [Range(0.01f, 1f)]
    public float speed = 0.02f;

    //used to effect velocity of oscillation of patrolling enemy
    Rigidbody rb;

    //vectors used to get the beginning and end of the platform to be patrolled
    Vector3 startPatrolling, endPatrolling;

    //sets default type of movement pattern here more to be added onto this later
    public MovementType enemyMoves = MovementType.Horizontal;

    // Use this for initialization
    void Start()
    {
        //assigns the beginning and end of the collider of the platform the enemy will be patrolling upon
        startPatrolling = enemyPost.startPatrol;
        endPatrolling = enemyPost.endPatrol;
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //adds xOffset to the beginnning and end of the enemy's patrol route
        startPatrolling.x = enemyPost.startPatrol.x + xOffset;
        endPatrolling.x = enemyPost.endPatrol.x - xOffset;
        //this is half distance because the sine wave is being used to create patrol path
        float distance = Mathf.Abs((endPatrolling.x - startPatrolling.x) / 2);

        //increases velocity and changes position of enemy on patrol route
        rb.velocity = new Vector3(distance * Mathf.Sin(Time.time * speed), 0f, 0f);
        rb.transform.position = this.transform.position;
    }
}
