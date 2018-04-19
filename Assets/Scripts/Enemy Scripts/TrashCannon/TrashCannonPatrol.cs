using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCannonPatrol : MonoBehaviour {
    public EnStatsData enStats;
    public MeshCollider visionCone;
    public GameObject rootObject;
    int multiplier = 1;
    float rotate = 0f;

    Ray ray = new Ray();
    Rigidbody myRb;
	// Use this for initialization
	void Start () {
        myRb = gameObject.transform.parent.gameObject.GetComponent<Rigidbody>();
        ray = new Ray(transform.position, Vector3.left);
    }

    void Turn()
    {
        multiplier *= -1;
        //rotate = (multiplier == -1) ? -180 : 180f;
        //rootObject.transform.Rotate(Vector3.up, rotate);
        myRb.transform.Rotate(Vector3.up, 180f);
    }

    // Update is called once per frame
    public void FixedUpdate () {
        //Debug.Log(myRb.velocity.x);
        RaycastHit hit;

        if (myRb.velocity.x > 0)
        {
            Debug.Log("Rigidbody movement is more than 0");
            ray = new Ray(transform.position, Vector3.right);
        }
        else if (myRb.velocity.x < 0)
        {
            Debug.Log("Rigidbody movement is less than 0");
            ray = new Ray(transform.position, Vector3.left);
        }

        if (Physics.Raycast(ray, out hit, enStats.minDistance))
        {
            if (hit.collider.tag == "EndPoint")
            {
                Debug.Log("Endpoint detected by Ray");
                Turn();
            }
        }
        Debug.DrawRay(gameObject.transform.position, ray.direction);
        myRb.velocity = Vector3.right* multiplier * Time.timeScale * enStats.speed;
	}
}