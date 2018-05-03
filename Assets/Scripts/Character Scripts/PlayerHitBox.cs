using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour {
    string attackTag ="";
    int damage = 3;
    /// <summary>
    /// Changes the tag of the hitbox/collider based on the 
    /// </summary>
    /// <param name="input"></param>
    public void On_ChangeTag_Received(string input)
    {
        attackTag = input;
        gameObject.tag = attackTag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HurtBox" && other.gameObject.layer == 12)
        {
            other.gameObject.GetComponent<Enemy>().EnemyTakeDamage(damage, attackTag);
        }
    }
}
