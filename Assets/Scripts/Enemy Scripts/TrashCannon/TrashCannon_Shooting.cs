using System;
using System.Collections;
using UnityEngine;

public class TrashCannon_Shooting : MonoBehaviour {

    public GameObject bullet;
    public GameObject gunBarrel;
    GameObject gunSights;
    public EnStatsData enemyStats;
    int fullClip = 0;
    PoolItem poolBullets;
    Rigidbody myRb;
    int direction = 1;

	// Use this for initialization
	void Start () {
        gunSights = gunBarrel.transform.GetChild(0).gameObject; //finds and identifies the child object as the "sight" used to figure out the direction character is facing
        fullClip = enemyStats.bulletAmount;
        poolBullets = new PoolItem(fullClip, bullet);
    }

    private void FixedUpdate()
    {
        direction = (gunSights.transform.position.x - gunBarrel.transform.position.x < 0) ? -1 : 1;
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    FireBall();
        //}
    }

    public void Firing()
    {
        FireBall();
    }
    void FireBall()
    {
        if (fullClip > 0)
        {
            try
            {
                GameObject temp = poolBullets.Get(gunBarrel.transform.position);
                temp.GetComponent<Rigidbody>().velocity = Vector3.right * direction * enemyStats.shotForce;
            }
            catch (System.NullReferenceException)
            {
                throw;
            }
            fullClip--;
        }
        else
        {
            for (int i = 0; i < enemyStats.bulletAmount; i++)
            {
                poolBullets.ReUse();
                fullClip++;
            }
        }
    }
}
