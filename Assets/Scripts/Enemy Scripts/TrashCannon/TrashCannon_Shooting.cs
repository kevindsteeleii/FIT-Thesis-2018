using System;
using System.Collections.Generic;
using UnityEngine;

public class TrashCannon_Shooting : MonoBehaviour {
    public GameObject enemyMeleeObject;

    public GameObject bullet;
    public GameObject gunBarrel;
    public EnStatsData enemyStats;
    int fullClip = 0;
    PoolItem poolBullets;

	// Use this for initialization
	void Start () {
        fullClip = enemyStats.bulletAmount;
        poolBullets = new PoolItem(fullClip, bullet);
    }

    private void Update()
    {
    }

    void Firing()
    {
        Debug.Log("Shooting from TrashCannon");
        if (fullClip > 0)
        {
            try
            {
                GameObject temp = poolBullets.Get(gunBarrel.transform.position);
                temp.GetComponent<Rigidbody>().velocity = Vector3.right * enemyStats.AttackInterval * Time.time;
            }
            catch (System.NullReferenceException)
            {
                throw;
            }
            fullClip--;
        }
        else
        {
            for(int i = 0; i < enemyStats.bulletAmount; i++)
            {
                poolBullets.ReUse();
                fullClip++;
            }
        }
    }
}
