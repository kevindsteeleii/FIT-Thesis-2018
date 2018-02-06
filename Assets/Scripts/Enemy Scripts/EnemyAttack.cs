using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField]
    BoxCollider hitBox;

    [Range(0, 25)]
    public int damage;

    bool alive;

    // Use this for initialization
    void Start () {

        alive = true;

		if (hitBox == null)
        {
            hitBox = gameObject.GetComponent<BoxCollider>();
        }
	}

    /// <summary>
    /// Uses a combination of overlapping hitboxes, layers, 
    /// and tags to determine if and when damage is done to the player
    /// </summary>
    void AutoAttack()
    {
        Collider[] cols = Physics.OverlapBox(hitBox.bounds.center, hitBox.bounds.extents, gameObject.transform.rotation, LayerMask.GetMask("Player"),QueryTriggerInteraction.Collide);
        foreach (Collider col in cols)
        {
            if (col.gameObject.tag == "Player")
            {
                col.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
       AutoAttack();
    }

}
