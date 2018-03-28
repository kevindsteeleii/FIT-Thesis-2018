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

    PoolItem poolBullets;

    int directionModifier = 1;
    //the number of shots that can be fired per second
    int fullClip = 0;
    //not in use atm, for the fire rate in seconds or 5 times a second .2 * 1.000 seconds
    [Range(0, 2)]
    public float fireRate = 0.2f;

    [Range(0, 100)]
    public float fireForce = 30f;

    bool enemyDetected = false; //used as a condition in the coroutines loop

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

        fullClip = Mathf.RoundToInt(1 / fireRate);
        poolBullets = new PoolItem(fullClip, bullet);

        //StartCoroutine(Fire_CoRoutine());
    }

    void FireFix() //using pooled objects to fire projectiles
    {
        GameObject temp = poolBullets.Get(gunBarrel.transform.position);
        temp.GetComponent<Rigidbody>().velocity = Vector3.right * directionModifier * fireForce;
    }

    // Update is called once per frame
    void Update()   {
        bool fired = false;
        //fires once per button press for now
        if (Input.GetKeyUp(KeyCode.Space) && !fired)
        {
            fired = true;
            FireFix();
        }
        while (enemyDetected)
        {
            Debug.Log("enemy detected");
            for (int i = 0; i < poolBullets.GetAmount(); i++)
            {
                FireFix();
                StartCoroutine(WaitXSeconds());
            }
            poolBullets.ReUse();
        }
    }

    private void FixedUpdate()  {
        //this if-else block deteremines a directional modifier for the launch of projectiles
        if (myRb.velocity.x > 0)    {
            directionModifier = 1;
        }
        else if (myRb.velocity.x < 0)   {
            directionModifier = -1;
        }

        
    }

    public void On_EnemyDetected_Received(bool did)
    {
        enemyDetected = did;
        Debug.Log("Enemy detection working... And enemy detected is " + enemyDetected);
    }
    IEnumerator WaitXSeconds()
    {
        yield return new WaitForSecondsRealtime(fireRate * 30 * Time.deltaTime);

    }
    //IEnumerator Fire_CoRoutine()
    //{
    //    while (enemyDetected == true)
    //    {
    //            Debug.Log("enemy detected");
    //            for (int i = 0; i < poolBullets.GetAmount(); i++)
    //            {
    //                FireFix();
    //                yield return new WaitForSecondsRealtime(fireRate * 30 * Time.deltaTime);
    //            }
    //            poolBullets.ReUse();
    //    }

    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject + " collider detected");
        if (other.tag == "EndPoint")
        {
            Debug.Log("EndPoint Detected");
            enemyDetected = true;
        }

        enemyDetected = false;
    }

    /****************************************TODO************************************************/
    /*3) create coroutine that has a fire rate when enemy is detected
      4) make all this conditional upon if the enemy detects a certain tagged object of colllider
      5) make some particle traces for the bullets
      6) figure out the grabbable projectiles
      7) figure out how to share ammo between enemies based on proximity and activity levels*/

}
