using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBox : MonoBehaviour {
    /// <summary>
    /// Event sent from hurtbox to the listener in the playerstats class to tally damage taken
    /// </summary>
    public event Action<int> On_Damage_Received;

    //an attempt to clean up how enemies interact with player character in an attack context
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hitting " + other.gameObject+" with tag "+ other.gameObject.tag);
        if (other.tag == "HitBox" && other.gameObject.layer == 12)
        {
            //Debug.Log("Enemy struck player!!"); //it works
        }
    }
}
