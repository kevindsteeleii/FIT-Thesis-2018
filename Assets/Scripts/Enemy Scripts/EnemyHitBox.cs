using System.Collections;
using System;
using UnityEngine;
/// <summary>
/// Component attached to gameobject hosting the hitbox/hit-collider
/// </summary>
public class EnemyHitBox : MonoBehaviour {
    public EnStatsData enemyStats;
    int damage = 0;
    int modifier = 1;
    public EnemyMeleeHitBox hitboxNumSender;

	// Use this for initialization
	void Start () {
        hitboxNumSender.On_HitBoxNumber_Sent += On_HitBoxNumber_Received;
        damage = enemyStats.dmg;    //once a reasonable amount of damage is established erased the copy/paste code in Update()
    }
	
	// Update is called once per frame
	void Update () {
        damage = enemyStats.dmg;    //to test in real time how much is an appropriate amount of damage
	}
    /// <summary>
    /// Subscriber to event that broadcasts number of hitboxes used/collided with in a melee attack
    /// </summary>
    /// <param name="num"></param>
    void On_HitBoxNumber_Received (int num)
    {
        modifier = num;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("Hit object with tag {0} on layer {1}", other.tag, other.gameObject.layer));
        if (other.tag == "Player")
        {
            if (other.gameObject.layer == 10)
            {
                Debug.Log("Hit player's hurtbox");
                //use direct function calls for damage because subscriber is always asynchronously sending data
                other.gameObject.transform.root.GetComponent<PlayerStats>().TakeDamage((int)damage/modifier);
            }
        }
    }
}
