using System.Collections;
using System;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour {
    public EnStatsData enemyStats;
    int damage = 0;
    int modifier = 1;
    public EnemyMeleeHitBox hitboxNumSender;

	// Use this for initialization
	void Start () {
        hitboxNumSender.On_HitBoxNumber_Sent += On_HitBoxNumber_Received;
	}
	
	// Update is called once per frame
	void Update () {
        damage = enemyStats.dmg;
	}
    /// <summary>
    /// Subscriber to event that broadcasts number of hitboxes used in a melee attack
    /// </summary>
    /// <param name="num"></param>
    void On_HitBoxNumber_Received (int num)
    {
        modifier = num;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.layer == 10)
            {
                //use direct function calls for damage because subscriber is always asynchronously sending data
                other.gameObject.transform.root.GetComponent<PlayerStats>().TakeDamage(damage/modifier);
            }
        }
    }
}
