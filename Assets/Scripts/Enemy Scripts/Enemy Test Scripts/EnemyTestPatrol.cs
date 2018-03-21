using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestPatrol : MonoBehaviour {

    public EnStatsData enStats;

    int multiplier = 1;

    Rigidbody myRB;

    // Use this for initialization
    void Start () {
		if (myRB != null)
        {
            return;
        }
        else
        {
            myRB = gameObject.GetComponent<Rigidbody>();
        }
	}

    void Turn()
    {
        multiplier *= -1;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = new Ray();

        if (myRB.velocity.x > 0)    //if the velocity is positive so is the ray's direction
        {
            ray = new Ray(transform.position, Vector3.right);
        }
        else
        {
            ray = new Ray(transform.position, Vector3.left);
        }

        if (Physics.Raycast(ray, out hit, enStats.minDistance))
        {
            if (hit.collider.tag == "EndPoint")
            {
                Turn();
            }
        }
        myRB.velocity = Vector3.right * Time.timeScale * enStats.speed * multiplier;
    }
}