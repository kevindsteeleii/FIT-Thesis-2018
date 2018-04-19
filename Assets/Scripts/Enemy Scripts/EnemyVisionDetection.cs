using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisionDetection : MonoBehaviour
{
    public EnStatsData enStats;

    int multiplier = 1; //used to start/stop the enemy
    int stopper = 1;
    Rigidbody myRB;
    public Animator enAnim;

    public MeshCollider visionCone;

    Quaternion currentRot;  //rotation to be set by the direction enemy moves in

    // Use this for initialization
    void Start()
    {
        if (myRB != null)
        {
            return;
        }
        else
        {
            myRB = gameObject.transform.root.GetComponent<Rigidbody>();
        }

        if (visionCone != null)
        {
            return;
        }
        else
        {
            visionCone = gameObject.GetComponentInChildren<MeshCollider>();
        }
    }

    protected virtual void Turn()
    {
        multiplier *= -1;
        gameObject.transform.rotation = currentRot;
    }

    protected virtual void FixedUpdate()
    {
        enAnim.SetFloat("speed", myRB.velocity.x);
        enAnim.SetFloat("vertSpeed", myRB.velocity.y);

        RaycastHit hit;
        Ray ray = new Ray();

        if (myRB.velocity.x > 0)    //if the velocity is positive so is the ray's direction
        {
            ray = new Ray(transform.position, Vector3.right);
            currentRot = new Quaternion(0, 0, 0, 1);
        }
        else
        {
            ray = new Ray(transform.position, Vector3.left);
            currentRot = new Quaternion(0, 180, 0, 1);
        }

        if (Physics.Raycast(ray, out hit, enStats.minDistance))
        {
            if (hit.collider.tag == "EndPoint")
            {
                Turn();
            }
        }
        Debug.DrawRay(gameObject.transform.root.position, ray.direction);
        myRB.velocity = Vector3.right * Time.timeScale * enStats.speed * multiplier;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enAnim.SetBool("enemyDetected", true);
        }
        else
        {
            enAnim.SetBool("enemyDetected", false);
        }
    }
    
    void TriggerMe()
    {
           
    }
}
