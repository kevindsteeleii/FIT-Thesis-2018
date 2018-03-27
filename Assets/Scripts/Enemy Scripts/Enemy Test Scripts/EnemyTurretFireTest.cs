using System;
using System.Collections;
using UnityEngine;

public class EnemyTurretFireTest : MonoBehaviour
{
    //rigid body of the parent enemy element to be used to determine direction of shot
    Rigidbody myRb;
    //commented out bullets array used for the grabbable and non-grabbable ammo types to be tested later
    //public GameObject[] bullets = new GameObject[2];

    //the empty game object used to launch bullets from
    public GameObject gunBarrel;
    public GameObject bullet;

    //not in use atm, for the fire rate in seconds or 5 times a second .2 * 1.000 seconds
    [Range(0, 2)]
    public float fireRate = 0.2f;

    [Range(0, 100)]
    public float fireForce = 30f;

    // Use this for initialization
    protected virtual void Start()
    {
        if (myRb != null)
        {
            return;
        }
        else
        {
            myRb = gameObject.GetComponent<Rigidbody>(); //to be used to determine direction which should be determined in fixed update
        }
    }
    private void Fire()
    {
        GameObject cannonBall = GameObject.Instantiate(bullet);
        //cannonBall.SetActive(false);
        cannonBall.transform.position = gunBarrel.transform.position;
        cannonBall.GetComponent<Rigidbody>().velocity = Vector3.left * fireForce;
    }
    // Update is called once per frame
    void Update()
    {
        bool fired = false;
        //fires once per button press for now
        if (Input.GetKeyUp(KeyCode.Space) && !fired)
        {
            fired = true;
            Fire();
        }
    }

    //TODO
   /*1) create a method by which the ammo fires in the direction of movement
     2) make adjustments to make ammo object pooled
     3) create coroutine that has a fire rate
     4) make all this conditional upon if the enemy detects a certain tagged object of colllider*/

}
